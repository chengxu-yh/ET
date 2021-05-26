namespace ET
{
    public static class GamerHelper
    {
        public static Gamer InitGamer(Entity domain, string account)
        {
            Gamer gamer = EntityFactory.Create<Gamer>(domain);
            // 首次初始化GAMER
            gamer.Namer = "Gamer_" + account;
            
            return gamer;
        }

        public static GamerInfo CreateGamerInfo(Gamer gamer)
        {
            GamerInfo gamerInfo = new GamerInfo();

            gamerInfo.GamerId = gamer.Id;
            gamerInfo.GamerName = gamer.Namer;

            return gamerInfo;
        }
    }
}