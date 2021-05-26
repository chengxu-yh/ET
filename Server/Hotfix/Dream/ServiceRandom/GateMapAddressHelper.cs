using System.Collections.Generic;
using System.Linq;

namespace ET
{
	public static class GateMapAddressHelper
	{
		public static long GetMapID(int zone)
		{
			List<StartSceneConfig> maps = StartSceneConfigCategory.Instance.Maps[zone];

			int n = RandomHelper.RandomNumber(0, maps.Count);

			return maps[n].SceneId;
		}
	}
}
