using UnityEngine;

namespace ET
{
    public class AfterShellCreate_CreateUnitView : AEvent<AppEventType.AfterShellCreate>
    {
        protected override async ETTask Run(AppEventType.AfterShellCreate args)
        {
            CreateShellViewAsync(args.Unit).Coroutine();

            await ETTask.CompletedTask;
        }

        private async ETTask CreateShellViewAsync(DUnit shell)
        {
            UShellConfig config = shell.GetComponent<UShellConfigComponent>().RoleConfig;

            GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset("Unit.unity3d", "Unit");
            GameObject prefab = bundleGameObject.Get<GameObject>(config.Model);

            GameObject go = UnityEngine.Object.Instantiate(prefab, GlobalComponent.Instance.Unit, true);
            go.transform.position = shell.Position;

            shell.AddComponent<GameObjectComponent>().GameObject = go;

            await ETTask.CompletedTask;
        }
    }
}