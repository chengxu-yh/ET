using UnityEngine;

namespace ET
{
    public class DChangeRotation_SyncGameObjectRotation: AEvent<AppEventType.ChangeRotation>
    {
        protected override async ETTask Run(AppEventType.ChangeRotation args)
        {
            GameObjectComponent gameObjectComponent = args.Unit.GetComponent<GameObjectComponent>();
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