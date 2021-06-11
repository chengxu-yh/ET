using UnityEngine;

namespace ET
{
    public static class TriggerFactory
    {
        public static Trigger Create(Entity parent, TriggerType type)
        {
            Trigger trigger = EntityFactory.CreateWithParent<Trigger, TriggerType>(parent, type);

            return trigger;
        }

    }
}
