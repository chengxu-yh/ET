namespace ET
{
    // 登录结束，创建大厅界面
    public class DLoginFinish_CreateLobbyUI : AEvent<AppEventType.LoginFinish>
	{
		protected override async ETTask Run(AppEventType.LoginFinish args)
		{
			await UIHelper.Create(Game.Scene, DUIType.UILobby);
		}
	}

    public class LobbyEnterSceneFinish_RemoveLobbyUI : AEvent<AppEventType.LobbyEnterSceneFinish>
    {
        protected override async ETTask Run(AppEventType.LobbyEnterSceneFinish args)
        {
            await UIHelper.Remove(Game.Scene, UIType.UILobby);
        }
    }

    public class DEnterMapFinish_RemoveLobbyUI : AEvent<AppEventType.EnterMapFinish>
	{
		protected override async ETTask Run(AppEventType.EnterMapFinish args)
		{
			// 加载场景资源
			await ResourcesComponent.Instance.LoadBundleAsync("map.unity3d");

			// 切换到map场景
			SceneChangeComponent sceneChangeComponent = Game.Scene.GetComponent<SceneChangeComponent>();
			await sceneChangeComponent.ChangeSceneAsync(args.SceneName);
		}
	}


	// GUI创建与销毁事件处理函数
	[UIEvent(DUIType.UILobby)]
	public class DUILobbyEvent : AUIEvent
	{
		public override async ETTask<UI> OnCreate(UIComponent uiComponent)
		{
			UI ui = await DUIHelper.InstantiateFromBundle(uiComponent, DUIType.UILobby);

			ui.AddComponent<DUILoginComponent>();

			return ui;
		}

		public override void OnRemove(UIComponent uiComponent)
		{
			DUIHelper.UnloadBundle(DUIType.UILobby);
		}
	}
}
