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
                await Game.EventSystem.Publish(new AppEventType.EnterSceneStart() { SceneName = scene });

                // 3 ��������Ԫ��


                // 4 ������ɫ


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