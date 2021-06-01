using UnityEngine;

namespace ET
{
    public static class TrapFactory
    {
        public static DUnit Create(Entity domain, DUnitInfo unitInfo)
        {
            DUnit trap = DUnitFactory.Create(domain, unitInfo.UnitId);

            // 位置信息
            trap.Position = new Vector3(unitInfo.PX, unitInfo.PY, unitInfo.PZ);
            trap.Rotation = new Quaternion(unitInfo.RX, unitInfo.RY, unitInfo.RZ, unitInfo.RW);
            // 配置信息
            trap.AddComponent<UTrapConfigComponent, int>(unitInfo.ConfigId);
            // 阵营信息
            trap.AddComponent<CampComponent, long, int>(unitInfo.GamerId, unitInfo.Camp);
            // 旋转组件
            trap.AddComponent<TurnComponent>();
            // 数值信息
            InitTrapNumberic(trap);

            // 触发创建完成事件
            Game.EventSystem.Publish(new AppEventType.AfterTrapCreate() { Unit = trap }).Coroutine();

            return trap;
        }

        private static void InitTrapNumberic(DUnit trap)
        {
            NumericComponent numeric = trap.AddComponent<NumericComponent>();
            UTrapConfig config = trap.GetComponent<UTrapConfigComponent>().RoleConfig;

            // 攻速
            numeric.Set(NumericType.AttackSpeedBase, config.AttackSpeed);
            // 攻击力
            numeric.Set(NumericType.HPDamageBase, config.HPDamage);
        }
    }
}
