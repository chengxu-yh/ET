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
            // 类型信息
            role.AddComponent<UnitTypeComponent, UnitType>(UnitType.UnitRole);
            // 配置信息
            role.AddComponent<URoleConfigComponent, int>(unitInfo.ConfigId);
            // 运算者
            role.AddComponent<OperationerComponent, long>(unitInfo.OperationerId);
            // 阵营信息
            role.AddComponent<CampComponent, long, CampType>(unitInfo.GamerId, (CampType)unitInfo.Camp);
            // 移动组件
            role.AddComponent<DMoveComponent>();
            // 旋转组件
            role.AddComponent<TurnComponent>();
            // 寻路组件
            role.AddComponent<PathComponent>();
            // 数值信息
            RoleHelper.InitRoleNumberic(role);
            // 技能组件
            role.AddComponent<SkillComponent>();

            // 触发创建完成事件
            Game.EventSystem.Publish(new AppEventType.AfterRoleCreate() { Unit = role }).Coroutine();

            return role;
        }

    }
}
