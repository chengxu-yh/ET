using System.Collections.Generic;

namespace ET
{
    public class GamerComponent: Entity
	{
		public readonly Dictionary<long, Gamer> idGamers = new Dictionary<long, Gamer>();

		public Gamer myGamer;

		public int Count
		{
			get
			{
				return this.idGamers.Count;
			}
		}
	}
}