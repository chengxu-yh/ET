namespace ET
{
    public sealed class Skill: Entity
    {
        public int ConfigId;            // 配置表id

        public SkillState SkillState;   // 技能当前状态

        public bool SkillLoop;          // 循环使用技能

        public SkillDamageType SkillDamageType; // 伤害类型

        public SkillConfig SkillConfig => SkillConfigCategory.Instance.Get(this.ConfigId);
    }

}