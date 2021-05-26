using System;


namespace ET
{
	[ActorMessageHandler]
	public class R2G_CreateAccountHandler : AMActorRpcHandler<Scene, R2G_CreateAccount, G2R_CreateAccount>
	{
		protected override async ETTask Run(Scene scene, R2G_CreateAccount request, G2R_CreateAccount response, Action reply)
		{
			// 1 首次创建Gamer对象
			Gamer gamer = GamerHelper.InitGamer(scene.Domain, request.Account);

			// 2 存出Gamer对象到DB
			DBComponent db = scene.GetComponent<DBComponent>();
			await GamerDBHelper.AddGamerToDB(db, gamer);

			// 3 创建Player对象
			DPlayer player = PlayerHelper.InitPlayer(request.Account, request.Password, gamer.Id);
			await PlayerDBHelper.AddPlayerToDB(db, player);

			reply();
		}
	}
}