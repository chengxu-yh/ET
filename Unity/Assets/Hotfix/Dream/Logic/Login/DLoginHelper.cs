using System;


namespace ET
{
    public static class DLoginHelper
    {
        public static async ETVoid Login(Scene zoneScene, string address, string account, string password)
        {
            try
            {
                // 创建一个ETModel层的Session
                R2C_DLogin r2CLogin;
                using (Session session = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address)))
                {
                    r2CLogin = (R2C_DLogin) await session.Call(new C2R_DLogin() { Account = account, Password = password });
                }

                // 创建一个gate Session,并且保存到SessionComponent中
                Session gateSession = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(r2CLogin.Address));
                gateSession.AddComponent<PingComponent>();
                zoneScene.AddComponent<SessionComponent>().Session = gateSession;
				
                G2C_DLoginGate g2CLoginGate = (G2C_DLoginGate)await gateSession.Call(
                    new C2G_DLoginGate() { Key = r2CLogin.Key, GateId = r2CLogin.GateId});

                Log.Info("登陆gate成功!");

                await LobbyHelper.EnterLobbyAsync(zoneScene);

                await Game.EventSystem.Publish(new AppEventType.LoginFinish() { ZoneScene = zoneScene });
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        } 
    }
}