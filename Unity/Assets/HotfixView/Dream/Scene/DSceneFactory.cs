namespace ET
{
    public static class DSceneFactory
    {
        public static Scene CreateZoneScene(int zone, string name)
        {
            Scene zoneScene = EntitySceneFactory.CreateScene(Game.IdGenerater.GenerateId(), zone, SceneType.Zone, name, Game.Scene);

            ResourcesComponent.Instance.LoadBundle("Role.unity3d");
            ResourcesComponent.Instance.LoadBundle("Shell.unity3d");
            ResourcesComponent.Instance.LoadBundle("Trap.unity3d");
            ResourcesComponent.Instance.LoadBundle("Tower.unity3d");
            ResourcesComponent.Instance.LoadBundle("Trigger.unity3d");

            zoneScene.AddComponent<ZoneSceneFlagComponent>();
            zoneScene.AddComponent<NetKcpComponent>();
            zoneScene.AddComponent<SceneChangeComponent>();
            zoneScene.AddComponent<DOperaComponent>();
            zoneScene.AddComponent<PVPComponent, bool>(true);
            zoneScene.AddComponent<GamerComponent>();
            zoneScene.AddComponent<DUnitComponent>();

            // mainScene.AddComponent<AIComponent, int>(1);

            // 初始化UI层
            zoneScene.AddComponent<UIEventComponent>();
            zoneScene.AddComponent<UIComponent>();

            return zoneScene;
        }
    }
}