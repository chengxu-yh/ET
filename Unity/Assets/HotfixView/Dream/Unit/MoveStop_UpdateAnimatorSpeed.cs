namespace ET
{
    public class MoveStop_UpdateAnimatorSpeed : AEvent<AppEventType.MoveStop>
    {
        protected override async ETTask Run(AppEventType.MoveStop args)
        {
            DAnimatorComponent animatorComponent = args.Unit.GetComponent<DAnimatorComponent>();
            if (animatorComponent == null)
            {
                return;
            }

            animatorComponent.SetFloatValue("Speed", 0);

            await ETTask.CompletedTask;
        }
    }
}