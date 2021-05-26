namespace ET
{
    namespace AppEventType
    {
        public struct AppStart { }

        public struct AppStartInitFinish { }

        public struct LoginFinish { }

        public struct EnterMapFinish 
        {
            public string SceneName; 
        }

        public struct LobbyEnterSceneFinish { }
    }
}