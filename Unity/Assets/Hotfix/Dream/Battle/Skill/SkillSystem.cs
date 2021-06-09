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
            self.SetSkillState(SkillState.SkillWait);

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

                if (status != behaviac.EBTStatus.BT_RUNNING)
                {
                    Debug.Log($"TreeState:{status},SkillState.SkillEnd");
                    self.SetSkillState(SkillState.SkillEnd);
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