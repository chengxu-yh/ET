

namespace ET
{
	public class DSessionPlayerComponentDestroySystem : DestroySystem<DSessionPlayerComponent>
	{
		public override void Destroy(DSessionPlayerComponent self)
		{
			// 发送断线消息
			ActorLocationSenderComponent.Instance.Send(self.Player.GamerId, new G2M_DSessionDisconnect());

			self.Domain.GetComponent<DPlayerComponent>()?.Remove(self.Player.Id);
		}
	}
}
