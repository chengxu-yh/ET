using System.Collections.Generic;

namespace ET
{
    public static class PlayerDBHelper
    {
        public static async ETTask<DPlayer> GetPlayerFromDB(DBComponent db, string account)
        {
            DPlayer player;
            List<DPlayer> playerList = await db.Query<DPlayer>(DPlayer => DPlayer.Account == account);

            if (playerList == null || playerList.Count == 0)
            {
                player = null;
            }
            else
            {
                player = playerList[0];
            }

            return player;
        }

        public static async ETTask AddPlayerToDB(DBComponent db, DPlayer player)
        {
            await db.Save(player);

            Log.Info($"–¬‘ˆ’À∫≈£∫{player.Account}£¨√‹¬Î£∫{player.PassWord}°£");
        }
    }
}