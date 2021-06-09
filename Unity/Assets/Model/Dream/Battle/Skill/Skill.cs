namespace ET
{
    public sealed class Skill: Entity
    {
        public int ConfigId;            // 配置表id

        public SkillState SkillState;   // 技能当前状态

        public SkillConfig SkillConfig => SkillConfigCategory.Instance.Get(this.ConfigId);
    }

}