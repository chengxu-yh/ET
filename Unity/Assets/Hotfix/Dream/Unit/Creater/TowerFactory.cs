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
            // 配置信息
            tower.AddComponent<UTowerConfigComponent, int>(unitInfo.ConfigId);
            // 阵营信息
            tower.AddComponent<CampComponent, long, int>(unitInfo.GamerId, unitInfo.Camp);
            // 数值信息
            InitRoleNumberic(tower);

            // 触发创建完成事件
            Game.EventSystem.Publish(new AppEventType.AfterTowerCreate() { Unit = tower }).Coroutine();

            return tower;
        }

        private static void InitRoleNumberic(DUnit role)
        {
            NumericComponent numeric = role.AddComponent<NumericComponent>();
            UTowerConfig config = role.GetComponent<UTowerConfigComponent>().RoleConfig;

            // 最大血量
            numeric.Set(NumericType.MaxHpBase, config.MaxHP);
            // 血量
            numeric.Set(NumericType.HpBase, config.HP);
            // 攻速
            numeric.Set(NumericType.AttackSpeedBase, config.AttackSpeed);
            // 血量恢复
            numeric.Set(NumericType.HPRegainBase, config.HPRegain);
            // 攻击力
            numeric.Set(NumericType.HPDamageBase, config.HPDamage);
            // 警戒范围
            numeric.Set(NumericType.AlertRadiusBase, config.AlertRadius);
        }
    }
}
