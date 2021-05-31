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

            GameObject go = trap.AddComponent<DGameObjectComponent>().Init("Unit.unity3d", "Unit", config.Model, GlobalComponent.Instance.Unit);
            go.AddComponent<ComponentView>().Component = trap;
            go.transform.position = trap.Position;

            trap.AddComponent<DAnimatorComponent>();

            await ETTask.CompletedTask;
        }
    }
}