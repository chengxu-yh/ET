namespace ET
{
    namespace AppEventType
    {
        public struct AppStart { }

        public struct AppStartInitFinish 
        {
            public Scene ZoneScene;
        }

        public struct LoginFinish 
        {
            public Scene ZoneScene;
        }

        public struct EnterSceneStart 
        {
            public Scene ZoneScene;
            public string SceneName; 
        }

        public struct LobbyEnterSceneFinish 
        {
            public Scene ZoneScene;
        }

        public struct EnterLevelFinish 
        {
            public Scene ZoneScene;
        }

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