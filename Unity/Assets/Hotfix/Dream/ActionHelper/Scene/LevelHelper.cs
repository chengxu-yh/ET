using System;


namespace ET
{
    public static class LevelHelper
    {
        public static async ETTask EnterLevelAsync(Scene zoneScene, int levelid)
        {
            try
            {
                // 1 ������ͨ��

                // 2 �л���ͼ
                string scene = "Map";
                await Game.EventSystem.Publish(new AppEventType.EnterSceneStart() { ZoneScene = zoneScene, SceneName = scene });

                // 3 ��������Ԫ��

                // 4 ������ɫ
                DUnitInfo roleinfo = new DUnitInfo();
                roleinfo.GamerId = zoneScene.Domain.GetComponent<GamerComponent>().myGamer.Id;
                roleinfo.UnitId = IdGenerater.Instance.GenerateUnitId(0);
                roleinfo.ConfigId = 1001;
                roleinfo.Camp = (int)(CampType.CampRed);
                roleinfo.PX = 0;
                roleinfo.PY = 0;
                roleinfo.PZ = 0;
                roleinfo.RX = 0;
                roleinfo.RY = 0;
                roleinfo.RZ = 0;
                roleinfo.RW = 1;

                RoleFactory.Create(zoneScene.Domain, roleinfo);

                // 5 ֪ͨ�ɹ�
                //await Game.EventSystem.Publish(new AppEventType.EnterLevelFinish());

            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }
    }
}