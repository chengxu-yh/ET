using UnityEngine;

namespace ET
{
    public class SkillRadiusTrigger : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            Game.EventSystem.Publish(new AppEventType.SkillRadiusEnter()
            {
                SelfGameObject = this.transform.parent.gameObject,
                OtherGameObject = other.gameObject
            }
            ).Coroutine();
        }

        public void OnTriggerExit(Collider other)
        {
            Game.EventSystem.Publish(new AppEventType.SkillRadiusExit()
            {
                SelfGameObject = this.transform.parent.gameObject,
                OtherGameObject = other.gameObject
            }
            ).Coroutine();
        }
    }

}