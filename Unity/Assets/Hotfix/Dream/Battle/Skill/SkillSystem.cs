using LitJson;
using UnityEngine;

namespace ET
{
    public class SkillAwakeSystem : AwakeSystem<Skill, int>
    {
        public override void Awake(Skill self, int configid)
        {
            self.ConfigId = configid;
            self.SetSkillState(SkillState.SkillWait);

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
            self.SkillLoop = self.SkillConfig.SkillLoop > 0 ? true : false;
            self.SkillDamageType = SkillDamageTypeHelper.GetSkillDamageType(self.SkillConfig.DamageType);

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


    }

}