using UnityEngine;

namespace ET
{
    public class DChangeRotation_SyncGameObjectRotation: AEvent<AppEventType.ChangeRotation>
    {
        protected override async ETTask Run(AppEventType.ChangeRotation args)
        {
            DGameObjectComponent gameObjectComponent = args.Unit.GetComponent<DGameObjectComponent>();
            if (gameObjectComponent == null)
            {
                return;
            }

            Transform transform = gameObjectComponent.GameObject.transform;
            transform.rotation = args.Unit.Rotation;
            await ETTask.CompletedTask;
        }
    }
}