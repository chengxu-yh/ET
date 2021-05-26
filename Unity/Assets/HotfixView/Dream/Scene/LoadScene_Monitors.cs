namespace ET
{
    public class EnterMapFinish_LoadScene : AEvent<AppEventType.EnterMapFinish>
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
}
