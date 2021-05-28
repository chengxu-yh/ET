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

        public struct LobbyEnterSceneFinish { }

        public struct EnterLevelFinish { }

        public struct ChangePosition
        {
            public DUnit Unit;
        }

        public struct ChangeRotation
        {
            public DUnit Unit;
        }

        public struct AfterRoleCreate
        {
            public DUnit Unit;
        }

        public struct AfterShellCreate
        {
            public DUnit Unit;
        }

        public struct AfterTrapCreate
        {
            public DUnit Unit;
        }
    }
}