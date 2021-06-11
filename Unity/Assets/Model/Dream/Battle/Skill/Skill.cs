namespace ET
{
    public sealed class Skill: Entity
    {
        // 所属角色
        public DUnit Self;

        // 技能当前目标角色
        public long TargetId;

        // 配置表id
        public int ConfigId;

        // 技能当前状态
        public SkillState SkillState;

        // 循环使用技能
        public bool SkillLoop;

        // 伤害类型
        public SkillDamageType SkillDamageType;

        // 目标阵营
        public int TargetCamp;

        // 目标类型
        public int TargetType;

        // 监控序列
        public ListComponent<long> MonitorList;

        // 伤害序列
        public ListComponent<long> DamageList;

        public SkillConfig SkillConfig => SkillConfigCategory.Instance.Get(this.ConfigId);
    }

}