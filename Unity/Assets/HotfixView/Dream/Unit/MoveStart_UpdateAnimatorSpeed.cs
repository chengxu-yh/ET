namespace ET
{
    public class MoveStart_UpdateAnimatorSpeed : AEvent<AppEventType.MoveStart>
    {
        protected override async ETTask Run(AppEventType.MoveStart args)
        {
            DAnimatorComponent animatorComponent = args.Unit.GetComponent<DAnimatorComponent>();
            if (animatorComponent == null)
            {
                return;
            }

            float speed = args.Unit.GetComponent<NumericComponent>().GetAsFloat(NumericType.Speed);
            animatorComponent.SetFloatValue("Speed", speed);

            await ETTask.CompletedTask;
        }
    }
}