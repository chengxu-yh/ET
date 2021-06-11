namespace ET
{
    public enum SkillDamageType
    {
        Target,             // 目标型
        TargetArea,         // 目标球形范围
        CasterArea,         // 自身球形范围
        CasterFront,        // 自身前方立方体
    }


    public static class SkillDamageTypeHelper
    {
        public static SkillDamageType GetSkillDamageType(string strtype)
        {
            SkillDamageType type = SkillDamageType.Target;

            switch (strtype.ToLower())
            {
                case "target":
                    type = SkillDamageType.Target;
                    break;
                case "targetarea":
                    type = SkillDamageType.TargetArea;
                    break;
                case "casterarea":
                    type = SkillDamageType.CasterArea;
                    break;
                case "casterfront":
                    type = SkillDamageType.CasterFront;
                    break;
            }

            return type;
        }
    }
}