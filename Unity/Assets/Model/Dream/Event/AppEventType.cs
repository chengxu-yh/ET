namespace ET
{
    namespace AppEventType
    {
        public struct AppStart { }

        public struct AppStartInitFinish { }

        public struct LoginFinish { }

        public struct EnterSceneStart 
        {
            public string SceneName; 
        }

        public struct EnterLevelFinish { }

        public struct LobbyEnterSceneFinish { }
    }
}