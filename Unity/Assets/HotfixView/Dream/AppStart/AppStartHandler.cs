namespace ET
{
    public class AppStartHandler: AEvent<AppEventType.AppStart>
    {
        protected override async ETTask Run(AppEventType.AppStart args)
        {
            Game.Scene.AddComponent<TimerComponent>();
            Game.Scene.AddComponent<CoroutineLockComponent>();

            Game.Scene.AddComponent<ResourcesComponent>();
            Game.Scene.AddComponent<ConfigComponent>();

            // 加载配置
            ResourcesComponent.Instance.LoadBundle("config.unity3d");
            ConfigComponent.GetAllConfigBytes = LoadConfigHelper.LoadAllConfigBytes;
            await ConfigComponent.Instance.LoadAsync();
            ResourcesComponent.Instance.UnloadBundle("config.unity3d");
            
            Game.Scene.AddComponent<OpcodeTypeComponent>();
            Game.Scene.AddComponent<MessageDispatcherComponent>();
            
            Game.Scene.AddComponent<NetThreadComponent>();

            Game.Scene.AddComponent<ZoneSceneManagerComponent>();
            
            Game.Scene.AddComponent<GlobalComponent>();

            Game.Scene.AddComponent<AIDispatcherComponent>();

            ResourcesComponent.Instance.LoadBundle("unit.unity3d");

            DSceneFactory.InitMainScene();

            await Game.EventSystem.Publish(new AppEventType.AppStartInitFinish());
        }
    }
}
