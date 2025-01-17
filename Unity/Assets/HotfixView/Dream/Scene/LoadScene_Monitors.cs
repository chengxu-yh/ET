﻿namespace ET
{
    public class EnterSceneStart_LoadScene : AEvent<AppEventType.EnterSceneStart>
	{
		protected override async ETTask Run(AppEventType.EnterSceneStart args)
		{
			// 加载场景AssetBundle
			await ResourcesComponent.Instance.LoadBundleAsync("map.unity3d");

			// 切换到map场景
			SceneChangeComponent sceneChangeComponent = args.ZoneScene.GetComponent<SceneChangeComponent>();
			await sceneChangeComponent.ChangeSceneAsync(args.SceneName);

			ResourcesComponent.Instance.UnloadBundle("map.unity3d");
		}
	}
}
