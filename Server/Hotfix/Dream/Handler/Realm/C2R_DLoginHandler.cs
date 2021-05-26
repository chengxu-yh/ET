using System;

namespace ET
{
    /// <summary>
    /// 登录REALM服务器，账号验证、注册KEY
    /// </summary>
    [MessageHandler]
    public class C2R_DLoginHandler : AMRpcHandler<C2R_DLogin, R2C_DLogin>
    {
        protected override async ETTask Run(Session session, C2R_DLogin request, R2C_DLogin response, Action reply)
        {
            // 随机分配一个Gate
            StartSceneConfig config = DRealmGateAddressHelper.GetGate(session.DomainZone());

            // 账号验证
            DBComponent db = session.Domain.GetComponent<DBComponent>();
            DPlayer player = await PlayerDBHelper.GetPlayerFromDB(db, request.Account);

            if (player == null)
            {
                // DB无Player对象，创建对象
                await ActorMessageSenderComponent.Instance.Call(
                    config.SceneId, new R2G_CreateAccount() { Account = request.Account, Password = request.Password });
            }
            else if(!player.PassWord.Equals(request.Password))
            {
                response.Error = ErrorCode.ERR_ConnectPasswordError;
                response.Message = "Password Error!";

                reply();
                return;
            }

            // 向gate请求一个key,客户端可以拿着这个key连接gate
            G2R_DGetLoginKey g2RGetLoginKey = (G2R_DGetLoginKey)await ActorMessageSenderComponent.Instance.Call(
                config.SceneId, new R2G_DGetLoginKey() { Account = request.Account });

            response.Address = config.OuterIPPort.ToString();
            response.Key = g2RGetLoginKey.Key;
            response.GateId = g2RGetLoginKey.GateId;

            reply();
        }

    }
}