using UnityEngine;

namespace ET
{
    public class AfterTrapCreate_CreateUnitView : AEvent<AppEventType.AfterTrapCreate>
    {
        protected override async ETTask Run(AppEventType.AfterTrapCreate args)
        {
            CreateTrapViewAsync(args.Unit).Coroutine();

            await ETTask.CompletedTask;
        }

        protected async ETTask CreateTrapViewAsync(DUnit trap)
        {
            URoleConfig config = trap.GetComponent<URoleConfigComponent>().RoleConfig;

            GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset("Unit.unity3d", "Unit");
            GameObject prefab = bundleGameObject.Get<GameObject>(config.Model);

            GameObject go = UnityEngine.Object.Instantiate(prefab, GlobalComponent.Instance.Unit, true);
            go.transform.position = trap.Position;

            trap.AddComponent<GameObjectComponent>().GameObject = go;
            trap.AddComponent<AnimatorComponent>();

            await ETTask.CompletedTask;
        }
    }
}