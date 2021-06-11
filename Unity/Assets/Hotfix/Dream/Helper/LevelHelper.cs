using System;


namespace ET
{
    public static class LevelHelper
    {
        public static async ETTask EnterLevelAsync(Scene zoneScene, int levelid)
        {
            try
            {
                // 1 服务器通信

                // 2 切换地图
                string scene = "Map";
                await Game.EventSystem.Publish(new AppEventType.EnterSceneStart() { ZoneScene = zoneScene, SceneName = scene });

                // 3 创建场景元素

                // 4 创建城堡
                DUnitInfo roleinfo1 = new DUnitInfo();
                roleinfo1.GamerId = zoneScene.Domain.GetComponent<GamerComponent>().myGamer.Id;
                roleinfo1.UnitId = IdGenerater.Instance.GenerateUnitId(0);
                roleinfo1.ConfigId = 1001;
                roleinfo1.Camp = (int)(CampType.CampRed);
                roleinfo1.PX = 25;
                roleinfo1.PY = 0;
                roleinfo1.PZ = 0;
                roleinfo1.RX = 0;
                roleinfo1.RY = 0;
                roleinfo1.RZ = 0;
                roleinfo1.RW = 1;
                roleinfo1.OperationerId = zoneScene.Domain.GetComponent<GamerComponent>().myGamer.Id;
                TowerFactory.Create(zoneScene.Domain, roleinfo1);

                DUnitInfo roleinfo2 = new DUnitInfo();
                roleinfo2.GamerId = zoneScene.Domain.GetComponent<GamerComponent>().myGamer.Id;
                roleinfo2.UnitId = IdGenerater.Instance.GenerateUnitId(0);
                roleinfo2.ConfigId = 1001;
                roleinfo2.Camp = (int)(CampType.CampGreen);
                roleinfo2.PX = -25;
                roleinfo2.PY = 0;
                roleinfo2.PZ = 0;
                roleinfo2.RX = 0;
                roleinfo2.RY = 0;
                roleinfo2.RZ = 0;
                roleinfo2.RW = 1;
                roleinfo2.OperationerId = zoneScene.Domain.GetComponent<GamerComponent>().myGamer.Id;
                TowerFactory.Create(zoneScene.Domain, roleinfo2);

                // 5 通知成功
                //await Game.EventSystem.Publish(new AppEventType.EnterLevelFinish());

            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }
    }
}