using UnityEngine;

namespace ET
{
    public static class GamerFactory
    {
        public static Gamer Create(Entity domain, GamerInfo gamerInfo)
        {
			Gamer gamer = EntityFactory.CreateWithId<Gamer, string>(domain, gamerInfo.GamerId, gamerInfo.GamerName);

	        GamerComponent gamerComponent = domain.GetComponent<GamerComponent>();
            gamerComponent.Add(gamer);
            
            return gamer;
        }
    }
}
