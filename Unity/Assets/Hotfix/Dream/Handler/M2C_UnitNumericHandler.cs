namespace ET
{
    [MessageHandler]
	public class M2C_UnitNumericHandler : AMHandler<M2C_UnitNumeric>
	{
		protected override async ETVoid Run(Session session, M2C_UnitNumeric message)
		{
			DUnit unit = session.Domain.GetComponent<DUnitComponent>().Get(message.Id);

			if (message.BeInt == true)
			{
				NumericAction.SetUnitNumericActionImp(unit, (NumericType)message.NumericType, (int)message.Val);
			}
			else
			{
				NumericAction.SetUnitNumericActionImp(unit, (NumericType)message.NumericType, message.Val);
			}

			await ETTask.CompletedTask;
		}
	}
}
