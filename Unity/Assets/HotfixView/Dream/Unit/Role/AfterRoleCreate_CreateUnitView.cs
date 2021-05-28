using UnityEngine;

namespace ET
{
    public class AfterRoleCreate_CreateUnitView : AEvent<AppEventType.AfterRoleCreate>
    {
        protected override async ETTask Run(AppEventType.AfterRoleCreate args)
        {
            CreateUnitViewAsync(args.Unit).Coroutine();

            await ETTask.CompletedTask;
        }

        protected async ETTask CreateUnitViewAsync(DUnit role)
        {
            URoleConfig config = role.GetComponent<URoleConfigComponent>().RoleConfig;

            GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset("Unit.unity3d", "Unit");
            GameObject prefab = bundleGameObject.Get<GameObject>(config.Model);

            GameObject go = UnityEngine.Object.Instantiate(prefab, GlobalComponent.Instance.Unit, true);
            go.transform.position = role.Position;

            role.AddComponent<GameObjectComponent>().GameObject = go;
            role.AddComponent<AnimatorComponent>();

            await ETTask.CompletedTask;
        }
    }
}