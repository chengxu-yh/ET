using System;


namespace ET
{
    public static class LobbyHelper
    {
        public static async ETTask<bool> EnterLobbyAsync(Scene zoneScene)
        {
            try
            {
                Session gateSession = zoneScene.GetComponent<SessionComponent>().Session;

                G2C_EnterLobby g2CEnterLobby = await gateSession.Call(new C2G_EnterLobby()) as G2C_EnterLobby;

                // 创建角色
                GamerFactory.Create(zoneScene.Domain, g2CEnterLobby.SelfGamer);

                Log.Info("登陆大厅成功!");

                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
        }
    }
}