namespace ET
{
    public static class PlayerHelper
    {
        public static DPlayer InitPlayer(string account, string password, long gamerid)
        {
            DPlayer player = EntityFactory.Create<DPlayer, string, string>(Game.Scene, account, password);

            player.GamerId = gamerid;
            
            return player;
        }

    }
}