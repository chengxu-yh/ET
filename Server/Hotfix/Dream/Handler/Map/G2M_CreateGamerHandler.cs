using System;

namespace ET
{
    [ActorMessageHandler]
	public class G2M_CreateGamerHandler : AMActorRpcHandler<Scene, G2M_CreateGamer, M2G_CreateGamer>
	{
		protected override async ETTask Run(Scene scene, G2M_CreateGamer request, M2G_CreateGamer response, Action reply)
		{
			DBComponent db = scene.GetComponent<DBComponent>();
			Gamer gamer = await GamerDBHelper.GetGamerFromDB(scene.Domain, db, request.GamerId);

			gamer.AddComponent<MailBoxComponent>();
			await gamer.AddLocation();

			gamer.AddComponent<GamerGateComponent, long>(request.GateSessionId);
			scene.GetComponent<GamerComponent>().Add(gamer);

			response.SelfGamer = GamerHelper.CreateGamerInfo(gamer);
			reply();
		}
	}
}