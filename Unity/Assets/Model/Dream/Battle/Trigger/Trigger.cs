namespace ET
{
    public enum TriggerType
    {
        SkillTrigger,
        AITrigger,
    }

    public sealed class Trigger: Entity
    {
        // 在Trigger内的对象
        public ListComponent<long> UnitList;

        // TriggerType
        public TriggerType TriggerType; 
    }
}