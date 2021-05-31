using UnityEngine;

namespace ET
{
    public static class RoleFactory
    {
        public static DUnit Create(Entity domain, DUnitInfo unitInfo)
        {
            DUnit role = DUnitFactory.Create(domain, unitInfo.UnitId);

            // 位置信息
            role.Position = new Vector3(unitInfo.PX, unitInfo.PY, unitInfo.PZ);
            role.Rotation = new Quaternion(unitInfo.RX, unitInfo.RY, unitInfo.RZ, unitInfo.RW);
            // 配置信息
            role.AddComponent<URoleConfigComponent, int>(unitInfo.ConfigId);
            // 阵营信息
            role.AddComponent<CampComponent, long, int>(unitInfo.GamerId, unitInfo.Camp);
            // 移动组件
            role.AddComponent<DMoveComponent>();
            // 数值信息
            InitRoleNumberic(role);

            // 触发创建完成事件
            Game.EventSystem.Publish(new AppEventType.AfterRoleCreate() { Unit = role }).Coroutine();

            return role;
        }

        private static void InitRoleNumberic(DUnit role)
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
            // 怒气
            numeric.Set(NumericType.AngerBase, config.Anger);
            // 怒气恢复
            numeric.Set(NumericType.AngerRegainBase, config.AngerRegain);
            // 血量恢复
            numeric.Set(NumericType.HPRegainBase, config.HPRegain);
            // 攻击力
            numeric.Set(NumericType.HPDamageBase, config.HPDamage);
            // 警戒范围
            numeric.Set(NumericType.AlertRadiusBase, config.AlertRadius);
        }
    }
}
