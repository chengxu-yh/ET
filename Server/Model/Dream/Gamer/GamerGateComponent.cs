namespace ET
{
	public class GamerGateComponentAwakeSystem : AwakeSystem<GamerGateComponent, long>
	{
		public override void Awake(GamerGateComponent self, long a)
		{
			self.Awake(a);
		}
	}

	public class GamerGateComponent : Entity, ISerializeToEntity
	{
		public long GateSessionActorId;

		public void Awake(long gateSessionId)
		{
			this.GateSessionActorId = gateSessionId;
		}
	}
}