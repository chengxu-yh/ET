using UnityEngine;

namespace ET
{
    public class DChangePosition_SyncGameObjectPos: AEvent<AppEventType.ChangePosition>
    {
        protected override async ETTask Run(AppEventType.ChangePosition args)
        {
            DGameObjectComponent gameObjectComponent = args.Unit.GetComponent<DGameObjectComponent>();
            if (gameObjectComponent == null)
            {
                return;
            }
            Transform transform = gameObjectComponent.GameObject.transform;
            transform.position = args.Unit.Position;
            await ETTask.CompletedTask;
        }
    }
}