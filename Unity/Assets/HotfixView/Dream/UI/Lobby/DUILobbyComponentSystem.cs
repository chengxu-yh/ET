﻿
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
            self.enterMap.GetComponent<Button>().onClick.AddListener(self.EnterMap);
        }
    }

    public static class DUILobbyComponentSystem
    {
        public static bool EnterLocked = false;

        public static void EnterMap(this DUILobbyComponent self)
        {
            EnterMapAsync(self).Coroutine();
        }

        public static async ETVoid EnterMapAsync(DUILobbyComponent ui)
        {
            await ETTask.CompletedTask;
            if (EnterLocked == true)
            {
                return;
            }
            EnterLocked = true;

            int levelid = 1;
            // 登录关卡
            await LevelHelper.EnterLevelAsync(ui.ZoneScene(), levelid);

            // 触发从Lobby登录关卡成功消息
            await Game.EventSystem.Publish(new AppEventType.LobbyEnterSceneFinish());

            EnterLocked = false;
        }
    }
}
