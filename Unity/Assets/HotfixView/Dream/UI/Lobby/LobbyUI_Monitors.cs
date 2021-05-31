namespace ET
{
    // 登录结束，创建大厅界面
    public class DLoginFinish_CreateLobbyUI : AEvent<AppEventType.LoginFinish>
	{
		protected override async ETTask Run(AppEventType.LoginFinish args)
		{
			await UIHelper.Create(args.ZoneScene, DUIType.UILobby);
		}
	}

    public class LobbyEnterSceneFinish_RemoveLobbyUI : AEvent<AppEventType.LobbyEnterSceneFinish>
    {
        protected override async ETTask Run(AppEventType.LobbyEnterSceneFinish args)
        {
            await UIHelper.Remove(args.ZoneScene, DUIType.UILobby);
        }
    }

	// GUI创建与销毁事件处理函数
	[UIEvent(DUIType.UILobby)]
	public class DUILobbyEvent : AUIEvent
	{
		public override async ETTask<UI> OnCreate(UIComponent uiComponent)
		{
			UI ui = await DUIHelper.InstantiateFromBundle(uiComponent, DUIType.UILobby);

			ui.AddComponent<DUILobbyComponent>();

			return ui;
		}

		public override void OnRemove(UIComponent uiComponent)
		{
			DUIHelper.UnloadBundle(DUIType.UILobby);
		}
	}
}
