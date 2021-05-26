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
                await Game.EventSystem.Publish(new AppEventType.EnterSceneStart() { SceneName = scene });

                // 3 创建场景元素


                // 4 创建角色


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