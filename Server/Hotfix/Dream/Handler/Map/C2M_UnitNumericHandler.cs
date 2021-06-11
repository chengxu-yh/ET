using UnityEngine;

namespace ET
{
    [ActorMessageHandler]
	public class C2M_UnitNumericHandler : AMActorLocationHandler<Gamer, C2M_UnitNumeric>
	{
		protected override async ETTask Run(Gamer gamer, C2M_UnitNumeric message)
		{
			M2C_UnitNumeric unitNumeric = new M2C_UnitNumeric();
			unitNumeric.Id = message.Id;
			unitNumeric.BeInt = message.BeInt;
			unitNumeric.NumericType = message.NumericType;
			unitNumeric.Val = message.Val;

			DMessageHelper.Broadcast(gamer, unitNumeric);
			
			await ETTask.CompletedTask;
		}
	}
}