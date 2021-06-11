namespace ET
{
    public enum SkillTargetCamp
    {
        Friend = 0,
        Enemy = 1,
    }

    public static class SkillTargetHelper
    {
        // 获取敌人友方模板数
        public static int GetTargetCamp(string camp)
        {
            int rescamp = 0;
            camp = camp.ToLower();

            if (camp.Contains("friend"))
            {
                rescamp += (1 << (int)(SkillTargetCamp.Friend));
            }

            if (camp.Contains("enemy"))
            {
                rescamp += (1 << (int)(SkillTargetCamp.Enemy));
            }
            
            return rescamp;
        }

        public static bool IsTargetCamp(int skilltgtcamp, CampType castercamp, CampType targetcamp)
        {
            bool res = false;
            if ((skilltgtcamp & (int)SkillTargetCamp.Friend) != 0)
            {
                if (castercamp == targetcamp)
                {
                    res = true;
                }
            }

            if ((skilltgtcamp & (int)SkillTargetCamp.Enemy) != 0)
            {
                if (castercamp != targetcamp)
                {
                    res = true;
                }
            }

            return res;
        }

        // 获取地面飞行模板值
        public static int GetTargetType(string type)
        {
            int restype = 0;
            type = type.ToLower();

            if (type.Contains("land"))
            {
                restype += (1 << (int)(UnitRoleType.Land));
            }

            if (type.Contains("enemy"))
            {
                restype += (1 << (int)(UnitRoleType.Sky));
            }

            return restype;
        }

        public static bool IsTargetType(int skilltgttype, UnitRoleType targettype)
        {
            bool res = false;

            if ((skilltgttype & (1 << (int)(targettype))) != 0)
            {
                res = true;
            }

            return res;
        }
    }
}