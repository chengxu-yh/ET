using UnityEngine;

namespace ET
{
    public class SkillDamageTrigger : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            Game.EventSystem.Publish(new AppEventType.SkillDamageEnter()
            {
                SelfGameObject = this.transform.parent.gameObject,
                OtherGameObject = other.gameObject
            }
            ).Coroutine();
        }

        public void OnTriggerExit(Collider other)
        {
            Game.EventSystem.Publish(new AppEventType.SkillDamageExit()
            {
                SelfGameObject = this.transform.parent.gameObject,
                OtherGameObject = other.gameObject
            }
            ).Coroutine();
        }
    }

}