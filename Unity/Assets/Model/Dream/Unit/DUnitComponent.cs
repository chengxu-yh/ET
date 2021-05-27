using System.Collections.Generic;

namespace ET
{

    public class DUnitComponent: Entity
	{
		public Dictionary<long, Unit> idUnits = new Dictionary<long, Unit>();
		public Unit MyUnit;
	}
}