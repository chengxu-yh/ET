using System;


namespace ET
{
	[MessageHandler]
	public class C2G_DLoginGateHandler : AMRpcHandler<C2G_DLoginGate, G2C_DLoginGate>
	{
		protected override async ETTask Run(Session session, C2G_DLoginGate request, G2C_DLoginGate response, Action reply)
		{
			Scene scene = session.DomainScene();
			string account = scene.GetComponent<DGateSessionKeyComponent>().Get(request.Key);
			if (account == null)
			{
				response.Error = ErrorCode.ERR_ConnectGateKeyError;
				response.Message = "Gate key验证失败!";
				reply();

				return;
			}

			DBComponent db = session.Domain.GetComponent<DBComponent>();
			DPlayer player = await PlayerDBHelper.GetPlayerFromDB(db, account);
			scene.GetComponent<DPlayerComponent>().Add(player);

			session.AddComponent<DSessionPlayerComponent>().Player = player;
			session.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);

			response.PlayerId = player.Id;
			reply();
		}
	}

}