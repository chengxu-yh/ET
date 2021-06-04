using UnityEngine;

namespace ET
{
    public class AfterTowerCreate_CreateUnitView : AEvent<AppEventType.AfterTowerCreate>
    {
        protected override async ETTask Run(AppEventType.AfterTowerCreate args)
        {
            CreateTowerViewAsync(args.Unit).Coroutine();

            await ETTask.CompletedTask;
        }

        private async ETTask CreateTowerViewAsync(DUnit role)
        {
            UTowerConfig config = role.GetComponent<UTowerConfigComponent>().TowerConfig;

            GameObject go = role.AddComponent<DGameObjectComponent>().Init("Tower.unity3d", "Tower", config.Model, GlobalComponent.Instance.Unit);
            go.AddComponent<ComponentView>().Component = role;
            go.transform.position = role.Position;

            // 声音组件
            role.AddComponent<SoundComponent>();

            await ETTask.CompletedTask;
        }
    }
}