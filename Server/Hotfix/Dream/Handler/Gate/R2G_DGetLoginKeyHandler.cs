using System;


namespace ET
{
	[ActorMessageHandler]
	public class R2G_DGetLoginKeyHandler : AMActorRpcHandler<Scene, R2G_DGetLoginKey, G2R_DGetLoginKey>
	{
		protected override async ETTask Run(Scene scene, R2G_DGetLoginKey request, G2R_DGetLoginKey response, Action reply)
		{
			long key = RandomHelper.RandInt64();
			scene.GetComponent<DGateSessionKeyComponent>().Add(key, request.Account);
			response.Key = key;
			response.GateId = scene.Id;

			reply();
			await ETTask.CompletedTask;
		}
	}
}