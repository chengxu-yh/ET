using LitJson;
using UnityEngine;

namespace ET
{
    public class SkillAwakeSystem : AwakeSystem<Skill, int>
    {
        public override void Awake(Skill self, int configid)
        {
            self.ConfigId = configid;

            self.MonitorList = ListComponent<long>.Create();
            self.DamageList = ListComponent<long>.Create();

            self.InitProperty();

            SkillAgent skillAgent = SkillAgentFactory.Create(self);
            if (skillAgent != null)
            {
                SkillSystem.Skills.Add(self.DomainZone(), self.Id, skillAgent);
            }
            else
            {
                Debug.LogError($"Create Skill Tree Failed, Skill ID:{configid}");
            }
        }
    }

    public class SkillDestroySystem : DestroySystem<Skill>
    {
        public override void Destroy(Skill self)
        {
            self.ConfigId = 0;

            self.Self = null;
            self.TargetId = 0;


            self.MonitorList.List.Clear();
            self.MonitorList = null;

            self.DamageList.List.Clear();
            self.DamageList = null;

            SkillSystem.Skills.Remove(self.DomainZone(), self.Id);
        }
    }

    public class SkillUpdateSystem : UpdateSystem<Skill>
    {
        public override void Update(Skill self)
        {
            if (self.GetSkillState() == SkillState.SkillEnd)
            {
                return;
            }

            self.UpdateTarget();

            SkillAgent skillAgent;
            SkillSystem.Skills.TryGetValue(self.DomainZone(), self.Id, out skillAgent);

            if (skillAgent != null)
            {
                behaviac.EBTStatus status = skillAgent.btexec();

                if (self.SkillLoop == false)
                {
                    if (status != behaviac.EBTStatus.BT_RUNNING)
                    {
                        Debug.Log($"TreeState:{status},SkillState.SkillEnd");
                        self.SetSkillState(SkillState.SkillEnd);
                    }
                }
            }
            else
            {
                self.SetSkillState(SkillState.SkillEnd);
            }
        }
    }


    public static class SkillSystem
    {
        public static MultiDictionary<int, long, SkillAgent> Skills = new MultiDictionary<int, long, SkillAgent>();

        public static void InitProperty(this Skill self)
        {
            // 所属Unit
            self.Self = self.GetParent<SkillComponent>().GetParent<DUnit>();
            // 技能状态初始化
            self.SetSkillState(SkillState.SkillWait);
            // 是否循环使用
            self.SkillLoop = self.SkillConfig.SkillLoop > 0 ? true : false;
            // 技能伤害类型
            self.SkillDamageType = SkillDamageTypeHelper.GetSkillDamageType(self.SkillConfig.DamageType);
            // 目标阵营
            self.TargetCamp = SkillTargetHelper.GetTargetCamp(self.SkillConfig.TargetCamp);
            // 目标类型
            self.TargetType = SkillTargetHelper.GetTargetType(self.SkillConfig.TargetType);

            NumericComponent numeric = self.AddComponent<NumericComponent>();
            SkillConfig config = self.SkillConfig;

            // 最大攻击半径
            numeric.Set(NumericType.MaxRadiusBase, config.MaxRadius);

            // 基础技能时长
            numeric.Set(NumericType.SkillTimeBase, config.SkillTime);

            // 伤害范围缩放
            if (self.SkillDamageType != SkillDamageType.Target)
            {
                JsonData damageScale = JsonMapper.ToObject(config.DamageScale);
                numeric.Set(NumericType.ScaleXBase, (float)damageScale["x"]);
                numeric.Set(NumericType.ScaleYBase, (float)damageScale["y"]);
                numeric.Set(NumericType.ScaleZBase, (float)damageScale["z"]);
            }

            // 触发创建完成事件
            Game.EventSystem.Publish(new AppEventType.AfterSkillCreate() { Skill = self }).Coroutine();
        }

        public static void SetSkillState(this Skill self, SkillState state)
        {
            self.SkillState = state;

            if (state == SkillState.SkillEnd)
            {
                SkillComponent component = self.GetParent<SkillComponent>();
                component.OnSkillEnd(self);
            }
        }

        public static SkillState GetSkillState(this Skill self)
        {
            return self.SkillState;
        }

        public static void EnterMonitorTrigger(this Skill self, DUnit unit)
        {
            // 如果符合阵营条件
            if (!SkillTargetHelper.IsTargetCamp(self.TargetCamp, self.Self.GetComponent<CampComponent>().Camp, unit.GetComponent<CampComponent>().Camp))
            {
                return;
            }

            // 如果符合类型条件
            UnitTypeComponent typeComponent = unit.GetComponent<UnitTypeComponent>();
            if (typeComponent.UnitType == UnitType.UnitRole)
            {
                URoleConfigComponent uRoleConfig = unit.GetComponent<URoleConfigComponent>();
                if (!SkillTargetHelper.IsTargetType(self.TargetType, uRoleConfig.RoleType))
                {
                    return;
                }
            }

            self.MonitorList.List.Add(unit.Id);
        }

        public static void ExitMonitorTrigger(this Skill self, DUnit unit)
        {
            if (self.MonitorList.List.Contains(unit.Id))
            {
                self.MonitorList.List.Remove(unit.Id);
            }
        }

        public static void EnterDamageTrigger(this Skill self, DUnit unit)
        {
            // 如果符合阵营条件
            if (!SkillTargetHelper.IsTargetCamp(self.TargetCamp, self.Self.GetComponent<CampComponent>().Camp, unit.GetComponent<CampComponent>().Camp))
            {
                return;
            }

            // 如果符合类型条件
            UnitTypeComponent typeComponent = unit.GetComponent<UnitTypeComponent>();
            if (typeComponent.UnitType == UnitType.UnitRole)
            {
                URoleConfigComponent uRoleConfig = unit.GetComponent<URoleConfigComponent>();
                if (!SkillTargetHelper.IsTargetType(self.TargetType, uRoleConfig.RoleType))
                {
                    return;
                }
            }

            self.DamageList.List.Add(unit.Id);
        }

        public static void ExitDamageTrigger(this Skill self, DUnit unit)
        {
            if (self.DamageList.List.Contains(unit.Id))
            {
                self.DamageList.List.Remove(unit.Id);
            }
        }

        public static DUnit GetMonitorTarget(this Skill self, SkillTargetCondition condition)
        {
            self.UpdateTriggers();

            DUnitComponent dUnitComponent = self.Domain.GetComponent<DUnitComponent>();
            if (condition == SkillTargetCondition.MaxDistance)
            {
                Vector3 srcpos = self.Self.Position;
                float sqrdis = 0;
                DUnit selunit = null;
                for (int i = 0; i < self.MonitorList.List.Count; i++)
                {
                    DUnit u = dUnitComponent.Get(self.MonitorList.List[i]);
                    float tmpdis = (u.Position - srcpos).sqrMagnitude;
                    if (tmpdis > sqrdis)
                    {
                        selunit = u;
                    }
                }
                return selunit;
            }
            else
            {
                Vector3 srcpos = self.Self.Position;
                float sqrdis = float.MaxValue;
                DUnit selunit = null;
                for (int i = 0; i < self.MonitorList.List.Count; i++)
                {
                    DUnit u = dUnitComponent.Get(self.MonitorList.List[i]);
                    float tmpdis = (u.Position - srcpos).sqrMagnitude;
                    if (tmpdis < sqrdis)
                    {
                        selunit = u;
                    }
                }
                return selunit;
            }
        }

        public static void SetSkillTarget(this Skill self, long unitid)
        {
            self.TargetId = unitid;
        }

        public static void UpdateTriggers(this Skill self)
        {
            ListComponent<long> deleteList = ListComponent<long>.Create();
            DUnitComponent dUnitComponent = self.Domain.GetComponent<DUnitComponent>();

            // 刷新MonitorList
            for (int i = 0; i < self.MonitorList.List.Count; i++)
            {
                DUnit unit = dUnitComponent.Get(self.MonitorList.List[i]);
                if (unit == null)
                {
                    deleteList.List.Add(unit.Id);
                    continue;
                }

                if (unit.GetComponent<UnitStateComponent>().UnitState == (int)UnitState.Death)
                {
                    deleteList.List.Add(unit.Id);
                }
            }
            for (int i = 0; i < deleteList.List.Count; i++)
            {
                self.MonitorList.List.Remove(deleteList.List[i]);
            }
            deleteList.List.Clear();


            // 刷新MonitorList
            for (int i = 0; i < self.DamageList.List.Count; i++)
            {
                DUnit unit = dUnitComponent.Get(self.DamageList.List[i]);
                if (unit == null)
                {
                    deleteList.List.Add(unit.Id);
                    continue;
                }

                if (unit.GetComponent<UnitStateComponent>().UnitState == (int)UnitState.Death)
                {
                    deleteList.List.Add(unit.Id);
                }
            }
            for (int i = 0; i < deleteList.List.Count; i++)
            {
                self.DamageList.List.Remove(deleteList.List[i]);
            }
            deleteList.List.Clear();
        }

        public static void UpdateTarget(this Skill self)
        {
            if (self.TargetId == 0)
            {
                return;
            }

            DUnitComponent dUnitComponent = self.Domain.GetComponent<DUnitComponent>();
            DUnit unit = dUnitComponent.Get(self.TargetId);
            if (unit == null)
            {
                self.SetSkillTarget(0);
                return;
            }
        }
    }

}