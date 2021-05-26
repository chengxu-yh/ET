namespace ET
{
    public static class DSceneFactory
    {
        public static Scene InitMainScene()
        {
            Scene mainScene = Game.Scene;
            mainScene.AddComponent<ZoneSceneFlagComponent>();
            mainScene.AddComponent<NetKcpComponent>();
            mainScene.AddComponent<SceneChangeComponent>();
            mainScene.AddComponent<OperaComponent>();

            mainScene.AddComponent<GamerComponent>(); 

            mainScene.AddComponent<UnitComponent>();
            mainScene.AddComponent<AIComponent, int>(1);

            // 初始化UI层
            mainScene.AddComponent<UIEventComponent>();
            mainScene.AddComponent<UIComponent>();

            return mainScene;
        }
    }
}