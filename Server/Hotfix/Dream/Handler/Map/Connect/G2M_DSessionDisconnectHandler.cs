

namespace ET
{
	[ActorMessageHandler]
	public class G2M_DSessionDisconnectHandler : AMActorLocationHandler<Gamer, G2M_DSessionDisconnect>
	{
		protected override async ETTask Run(Gamer gamer, G2M_DSessionDisconnect message)
		{
			await ETTask.CompletedTask;

			GamerComponent gamerComponent = gamer.DomainScene().GetComponent<GamerComponent>();
			if (gamerComponent != null)
			{
				gamerComponent.Remove(gamer.Id);
			}
		}
	}
}