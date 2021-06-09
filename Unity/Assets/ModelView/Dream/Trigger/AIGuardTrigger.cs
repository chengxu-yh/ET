using UnityEngine;

namespace ET
{
    public class AIGuardTrigger : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            Game.EventSystem.Publish(new AppEventType.AIGuardEnter()
            {
                SelfGameObject = this.transform.parent.gameObject,
                OtherGameObject = other.gameObject
            }
            ).Coroutine();
        }

        public void OnTriggerExit(Collider other)
        {
            Game.EventSystem.Publish(new AppEventType.AIGuardExit()
            {
                SelfGameObject = this.transform.parent.gameObject,
                OtherGameObject = other.gameObject
            }
            ).Coroutine();
        }
    }

}