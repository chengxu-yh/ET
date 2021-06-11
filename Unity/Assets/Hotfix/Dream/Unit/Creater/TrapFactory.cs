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
            // 类型信息
            trap.AddComponent<UnitTypeComponent, UnitType>(UnitType.UnitTrap);
            // 配置信息
            trap.AddComponent<UTrapConfigComponent, int>(unitInfo.ConfigId);
            // 运算者
            trap.AddComponent<OperationerComponent, long>(unitInfo.OperationerId);
            // 阵营信息
            trap.AddComponent<CampComponent, long, CampType>(unitInfo.GamerId, (CampType)unitInfo.Camp);
            // 旋转组件
            trap.AddComponent<TurnComponent>();
            // 数值信息
            TrapHelper.InitTrapNumberic(trap);

            // 触发创建完成事件
            Game.EventSystem.Publish(new AppEventType.AfterTrapCreate() { Unit = trap }).Coroutine();

            return trap;
        }

        
    }
}
