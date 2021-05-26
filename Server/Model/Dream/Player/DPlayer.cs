namespace ET
{
	public class DPlayerSystem : AwakeSystem<DPlayer, string, string>
	{
		public override void Awake(DPlayer self, string a, string p)
		{
			self.Awake(a, p);
		}
	}

	/// <summary>
	/// DPlayer是生存在GATE服上的玩家实例
	/// </summary>
	public sealed class DPlayer : Entity
	{
		public string Account { get; private set; }

		public string PassWord { get; private set; }

		public long GamerId { get; set; }

		public void Awake(string account, string password)
		{
			this.Account = account;

			this.PassWord = password;
		}
	}
}