using System.Net;

namespace ET
{
    public static class DSceneFactory
    {
        public static async ETTask<Scene> Create(Entity parent, string name, SceneType sceneType)
        {
            long id = IdGenerater.Instance.GenerateId();
            return await Create(parent, id, parent.DomainZone(), name, sceneType);
        }

        public static async ETTask<Scene> Create(Entity parent, long id, int zone, string name, SceneType sceneType, StartSceneConfig startSceneConfig = null)
        {
            await ETTask.CompletedTask;
            Scene scene = EntitySceneFactory.CreateScene(id, zone, sceneType, name);
            scene.Parent = parent;

            scene.AddComponent<MailBoxComponent, MailboxType>(MailboxType.UnOrderMessageDispatcher);

            switch (scene.SceneType)
            {
                case SceneType.Realm:
                    scene.AddComponent<NetKcpComponent, IPEndPoint>(startSceneConfig.OuterIPPort);
                    scene.AddComponent<DBComponent, string, string>(startSceneConfig.StartZoneConfig.DBConnection, startSceneConfig.StartZoneConfig.DBName);
                    break;
                case SceneType.Gate:
                    scene.AddComponent<NetKcpComponent, IPEndPoint>(startSceneConfig.OuterIPPort);
                    scene.AddComponent<DBComponent, string, string>(startSceneConfig.StartZoneConfig.DBConnection, startSceneConfig.StartZoneConfig.DBName);
                    scene.AddComponent<DPlayerComponent>();
                    scene.AddComponent<DGateSessionKeyComponent>();
                    break;
                case SceneType.Map:
                    scene.AddComponent<DBComponent, string, string>(startSceneConfig.StartZoneConfig.DBConnection, startSceneConfig.StartZoneConfig.DBName);
                    scene.AddComponent<GamerComponent>();
                    scene.AddComponent<UnitComponent>();
                    scene.AddComponent<RecastPathComponent>();
                    break;
                case SceneType.Location:
                    scene.AddComponent<LocationComponent>();
                    break;
            }

            return scene;
        }
    }
}