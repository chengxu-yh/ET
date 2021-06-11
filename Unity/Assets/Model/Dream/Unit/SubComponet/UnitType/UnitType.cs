namespace ET
{
    public enum UnitType
    {
        UnitRole,
        UnitShell,
        UnitTower,
        UnitTrap,
    }

    public enum UnitRoleType
    {
        Land = 0,
        Sky = 1,
    }

    public static class UnitRoleTypeHelper
    {
        public static UnitRoleType GetRoleType(string roletype)
        {
            UnitRoleType res = UnitRoleType.Land;
            switch (roletype.ToLower())
            {
                case "land":
                    res = UnitRoleType.Land;
                    break;
                case "sky":
                    res = UnitRoleType.Sky;
                    break;
            }
            return res;
        }
    }
    
}