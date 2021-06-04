namespace ET
{
    public static class TrapHelper
    {
        public static void InitTrapNumberic(DUnit trap)
        {
            NumericComponent numeric = trap.AddComponent<NumericComponent>();
            UTrapConfig config = trap.GetComponent<UTrapConfigComponent>().TrapConfig;

            // 攻速
            numeric.Set(NumericType.AttackSpeedBase, config.AttackSpeed);
            // 攻击力
            numeric.Set(NumericType.HPDamageBase, config.HPDamage);
        }
    }
}
