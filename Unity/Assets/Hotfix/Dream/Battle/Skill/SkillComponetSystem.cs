using LitJson;

namespace ET
{
    public class SkillComponentAwakeSystem : AwakeSystem<SkillComponent>
    {
        public override void Awake(SkillComponent self)
        {
            self.Skills = ListComponent<Skill>.Create();
            self.EndSkills = ListComponent<Skill>.Create();

            self.InitSkills();
        }
    }

    public class SkillComponentDestroySystem : DestroySystem<SkillComponent>
    {
        public override void Destroy(SkillComponent self)
        {
            for (int i = 0; i < self.Skills.List.Count; i++)
            {
                self.Skills.List[i].Dispose();
            }

            self.Skills.List.Clear();
            self.Skills = null;


            for (int i = 0; i < self.EndSkills.List.Count; i++)
            {
                self.EndSkills.List[i].Dispose();
            }
            self.EndSkills.List.Clear();
            self.EndSkills = null;
        }
    }

    public class SkillComponentUpdateSystem : UpdateSystem<SkillComponent>
    {
        public override void Update(SkillComponent self)
        {
            for (int i = 0; i < self.EndSkills.List.Count; i++)
            {
                self.EndSkills.List[i].Dispose();
            }
            self.EndSkills.List.Clear();
        }
    }

    public static class SkillComponentSystem
    {
        public static void InitSkills(this SkillComponent self)
        {
            DUnit role = self.GetParent<DUnit>();
            UnitTypeComponent typeComponent = role.GetComponent<UnitTypeComponent>();
            JsonData parameter = null;
            if (typeComponent.UnitType == UnitType.UnitRole)
            {
                parameter = JsonMapper.ToObject(role.GetComponent<URoleConfigComponent>().RoleConfig.Skills);
            }
            else if (typeComponent.UnitType == UnitType.UnitTower)
            {
                parameter = JsonMapper.ToObject(role.GetComponent<UTowerConfigComponent>().TowerConfig.Skills);
            }
            else if (typeComponent.UnitType == UnitType.UnitTrap)
            {
                parameter = JsonMapper.ToObject(role.GetComponent<UTrapConfigComponent>().TrapConfig.Skills);
            }
            else
            {
                parameter = JsonMapper.ToObject(role.GetComponent<UShellConfigComponent>().ShellConfig.Skills);
            }

            JsonData skills = parameter["skills"];
            if (skills != null)
            {
                for (int i = 0; i < skills.Count; i++)
                {
                    int skillid = (int)skills[i];
                    self.AddSkill(skillid);
                }
            }
        }

        public static void AddSkill(this SkillComponent self, int configid)
        {
            Skill skill = SkillFactory.Create(self, configid);
            self.Skills.List.Add(skill);
        }

        public static void OnSkillEnd(this SkillComponent self, Skill skill)
        {
            if (self.Skills.List.Contains(skill))
            {
                self.Skills.List.Remove(skill);
                self.EndSkills.List.Add(skill);
            }
        }
    }
}