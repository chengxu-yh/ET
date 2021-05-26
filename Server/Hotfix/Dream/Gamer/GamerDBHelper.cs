namespace ET
{
    public static class GamerDBHelper
    {
        public static async ETTask<Gamer> GetGamerFromDB(Entity domain, DBComponent db, long gamerid)
        {
            Gamer gamer = await db.Query<Gamer>(gamerid);
            gamer.Domain = domain;

            return gamer;
        }

        public static async ETTask AddGamerToDB(DBComponent db, Gamer gamer)
        {
            await db.Save(gamer);
            Log.Info($"ÐÂÔöÍæ¼Ò£º{gamer.Namer}¡£");
        }
    }
}