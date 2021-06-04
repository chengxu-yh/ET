namespace ET
{
    public static class RoleHelper
    {
        public static void InitRoleNumberic(DUnit role)
        {
            NumericComponent numeric = role.AddComponent<NumericComponent>();
            URoleConfig config = role.GetComponent<URoleConfigComponent>().RoleConfig;

            // 最大血量
            numeric.Set(NumericType.MaxHpBase, config.HP);
            // 血量
            numeric.Set(NumericType.HpBase, config.HP);
            // 速度
            numeric.Set(NumericType.SpeedBase, config.MoveSpeed);
            // 攻速
            numeric.Set(NumericType.AttackSpeedBase, config.AttackSpeed);
            // 攻击力
            numeric.Set(NumericType.HPDamageBase, config.HPDamage);
            // 警戒范围
            numeric.Set(NumericType.AlertRadiusBase, config.AlertRadius);
        }
    }
}
