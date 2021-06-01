using UnityEngine;

namespace ET
{
    [ActorMessageHandler]
	public class C2M_DPathfindingResultHandler : AMActorLocationHandler<Gamer, C2M_DPathfindingResult>
	{
		protected override async ETTask Run(Gamer gamer, C2M_DPathfindingResult message)
		{
			Vector3 target = new Vector3(message.X, message.Y, message.Z);

			// 广播寻路路径
			M2C_DPathfindingResult m2CPathfindingResult = new M2C_DPathfindingResult();
			m2CPathfindingResult.Id = message.Id;
			m2CPathfindingResult.X = message.X;
			m2CPathfindingResult.Y = message.Y;
			m2CPathfindingResult.Z = message.Z;
			for (int i = 0; i < message.Xs.Count; ++i)
			{
				m2CPathfindingResult.Xs.Add(message.Xs[i]);
				m2CPathfindingResult.Ys.Add(message.Ys[i]);
				m2CPathfindingResult.Zs.Add(message.Zs[i]);
			}

			DMessageHelper.Broadcast(gamer, m2CPathfindingResult);
			
			await ETTask.CompletedTask;
		}
	}
}