
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public class DUILobbyComponentAwakeSystem : AwakeSystem<DUILobbyComponent>
    {
        public override void Awake(DUILobbyComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            self.enterMap = rc.Get<GameObject>("EnterMap");
            self.enterMap.GetComponent<Button>().onClick.AddListener(self.EnterLevel);
        }
    }

    public static class DUILobbyComponentSystem
    {
        public static bool EnterLevelLocked = false;

        public static void EnterLevel(this DUILobbyComponent self)
        {
            EnterMapAsync(self).Coroutine();
        }

        private static bool LockEnterLevel()
        {
            if (EnterLevelLocked == true)
            {
                return false;
            }

            EnterLevelLocked = true;
            return true;
        }

        private static void UnLockEnterLevel()
        {
            EnterLevelLocked = false;
        }

        public static async ETVoid EnterMapAsync(DUILobbyComponent ui)
        {
            await ETTask.CompletedTask;
            if (!LockEnterLevel())
            {
                return;
            }

            int levelid = 1;
            // 登录关卡
            await LevelHelper.EnterLevelAsync(ui.ZoneScene(), levelid);

            // 触发从Lobby登录关卡成功消息
            await Game.EventSystem.Publish(new AppEventType.LobbyEnterSceneFinish());

            UnLockEnterLevel();
        }
    }
}
