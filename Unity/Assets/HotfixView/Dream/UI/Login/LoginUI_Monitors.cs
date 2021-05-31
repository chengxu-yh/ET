using UnityEngine;

namespace ET
{
	// DAppStartInitFinish事件，初始化完成，创建UI登录界面
	public class DAppStartInitFinish_CreateLoginUI: AEvent<AppEventType.AppStartInitFinish>
	{
		protected override async ETTask Run(AppEventType.AppStartInitFinish args)
		{
			await DUIHelper.Create(args.ZoneScene, DUIType.UILogin);
		}
	}


	// DLoginFinish事件，移除UI
	public class DLoginFinish_RemoveLoginUI : AEvent<AppEventType.LoginFinish>
	{
		protected override async ETTask Run(AppEventType.LoginFinish args)
		{
			await DUIHelper.Remove(args.ZoneScene, DUIType.UILogin);
		}
	}

	// GUI创建与销毁事件处理函数
	[UIEvent(DUIType.UILogin)]
	public class DUILoginEvent : AUIEvent
	{
		public override async ETTask<UI> OnCreate(UIComponent uiComponent)
		{
			UI ui = await DUIHelper.InstantiateFromBundle(uiComponent, DUIType.UILogin);

			ui.AddComponent<DUILoginComponent>();

			return ui;
		}

		public override void OnRemove(UIComponent uiComponent)
		{
			DUIHelper.UnloadBundle(DUIType.UILogin);
		}
	}
}
