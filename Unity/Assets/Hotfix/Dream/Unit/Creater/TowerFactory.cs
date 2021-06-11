using UnityEngine;

namespace ET
{
    public static class TowerFactory
    {
        public static DUnit Create(Entity domain, DUnitInfo unitInfo)
        {
            DUnit tower = DUnitFactory.Create(domain, unitInfo.UnitId);

            // 位置信息
            tower.Position = new Vector3(unitInfo.PX, unitInfo.PY, unitInfo.PZ);
            tower.Rotation = new Quaternion(unitInfo.RX, unitInfo.RY, unitInfo.RZ, unitInfo.RW);
            // 类型信息
            tower.AddComponent<UnitTypeComponent, UnitType>(UnitType.UnitTower);
            // 配置信息
            tower.AddComponent<UTowerConfigComponent, int>(unitInfo.ConfigId);
            // 运算者
            tower.AddComponent<OperationerComponent, long>(unitInfo.OperationerId);
            // 阵营信息
            tower.AddComponent<CampComponent, long, CampType>(unitInfo.GamerId, (CampType)unitInfo.Camp);
            // 数值信息
            TowerHelper.InitTowerNumberic(tower);
            // 血量恢复
            NumericComponent numeric = tower.GetComponent<NumericComponent>();
            tower.AddComponent<HPRegainComponent, int>(numeric.GetAsInt(NumericType.HPRegain));

            // 触发创建完成事件
            Game.EventSystem.Publish(new AppEventType.AfterTowerCreate() { Unit = tower }).Coroutine();

            return tower;
        }

        
    }
}
