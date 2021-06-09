using UnityEngine;

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

        public struct AfterTowerCreate
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

        public struct MoveStart
        {
            public DUnit Unit;
        }

        public struct MoveStop
        {
            public DUnit Unit;
        }

        public struct AfterSkillCreate
        {
            public Skill Skill;
        }

        public struct SkillRadiusEnter
        {
            public GameObject SelfGameObject;
            public GameObject OtherGameObject;
        }

        public struct SkillRadiusExit
        {
            public GameObject SelfGameObject;
            public GameObject OtherGameObject;
        }

        public struct SkillDamageEnter
        {
            public GameObject SelfGameObject;
            public GameObject OtherGameObject;
        }

        public struct SkillDamageExit
        {
            public GameObject SelfGameObject;
            public GameObject OtherGameObject;
        }

        public struct AIGuardEnter
        {
            public GameObject SelfGameObject;
            public GameObject OtherGameObject;
        }

        public struct AIGuardExit
        {
            public GameObject SelfGameObject;
            public GameObject OtherGameObject;
        }
    }
}