using System;


namespace ET
{
	[MessageHandler]
	public class C2G_EnterLobbyHandler : AMRpcHandler<C2G_EnterLobby, G2C_EnterLobby>
	{
		protected override async ETTask Run(Session session, C2G_EnterLobby request, G2C_EnterLobby response, Action reply)
		{
			DPlayer player = session.GetComponent<DSessionPlayerComponent>().Player;

			// 在随机选择的map服务器上创建Gamer
			long mapInstanceId = GateMapAddressHelper.GetMapID(session.DomainZone());

			M2G_CreateGamer createGamer = (M2G_CreateGamer)await ActorMessageSenderComponent.Instance.Call(
				mapInstanceId, new G2M_CreateGamer() { GamerId = player.GamerId, GateSessionId = session.InstanceId });

			// 返回信息
			response.SelfGamer = createGamer.SelfGamer;

			reply();
		}
	}
}