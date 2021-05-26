using System.Collections.Generic;
using System.Linq;

namespace ET
{
	public class DPlayerComponentSystem : AwakeSystem<DPlayerComponent>
	{
		public override void Awake(DPlayerComponent self)
		{
			self.Awake();
		}
	}
	
	public class DPlayerComponent : Entity
	{
		public static DPlayerComponent Instance { get; private set; }
		
		private readonly Dictionary<long, DPlayer> idPlayers = new Dictionary<long, DPlayer>();

		public void Awake()
		{
			Instance = this;
		}
		
		public void Add(DPlayer player)
		{
			this.idPlayers.Add(player.Id, player);
		}

		public DPlayer Get(long id)
		{
			this.idPlayers.TryGetValue(id, out DPlayer gamer);
			return gamer;
		}

		public void Remove(long id)
		{
			this.idPlayers.Remove(id);
		}

		public int Count
		{
			get
			{
				return this.idPlayers.Count;
			}
		}

		public DPlayer[] GetAll()
		{
			return this.idPlayers.Values.ToArray();
		}

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			
			base.Dispose();

			foreach (DPlayer player in this.idPlayers.Values)
			{
				player.Dispose();
			}

			this.idPlayers.Clear();

			Instance = null;
		}
	}
}