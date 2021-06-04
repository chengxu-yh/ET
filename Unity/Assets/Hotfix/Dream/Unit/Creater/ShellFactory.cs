using UnityEngine;

namespace ET
{
    public static class ShellFactory
    {
        public static DUnit Create(Entity domain, DUnitInfo unitInfo)
        {
            DUnit shell = DUnitFactory.Create(domain, unitInfo.UnitId);

            // 位置信息
            shell.Position = new Vector3(unitInfo.PX, unitInfo.PY, unitInfo.PZ);
            shell.Rotation = new Quaternion(unitInfo.RX, unitInfo.RY, unitInfo.RZ, unitInfo.RW);
            // 类型信息
            shell.AddComponent<UnitTypeComponent, UnitType>(UnitType.UnitShell);
            // 配置信息
            shell.AddComponent<UShellConfigComponent, int>(unitInfo.ConfigId);
            // 阵营信息
            shell.AddComponent<CampComponent, long, CampType>(unitInfo.GamerId, (CampType)unitInfo.Camp);
            // 数值信息
            ShellHelper.InitShellNumberic(shell);

            // 触发创建完成事件
            Game.EventSystem.Publish(new AppEventType.AfterShellCreate() { Unit = shell }).Coroutine();

            return shell;
        }

        
    }
}
