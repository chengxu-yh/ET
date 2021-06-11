using UnityEngine;

namespace ET
{
	[MessageHandler]
	public class M2C_DPathfindingResultHandler : AMHandler<M2C_DPathfindingResult>
	{
		protected override async ETVoid Run(Session session, M2C_DPathfindingResult message)
		{
			DUnit unit = session.Domain.GetComponent<DUnitComponent>().Get(message.Id);

			Vector3 servierpos = new Vector3(message.X, message.Y, message.Z);
			using var list = ListComponent<Vector3>.Create();

			for (int i = 0; i < message.Xs.Count; ++i)
			{
				list.List.Add(new Vector3(message.Xs[i], message.Ys[i], message.Zs[i]));
			}

			await DMoveAction.MoveActionImpAsync(unit, list.List.ToArray(), servierpos);
		}
	}
}
