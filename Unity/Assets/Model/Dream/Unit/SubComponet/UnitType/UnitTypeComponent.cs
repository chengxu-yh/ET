namespace ET
{
    public class UnitTypeComponentAwakeSystem : AwakeSystem<UnitTypeComponent, UnitType>
    {
        public override void Awake(UnitTypeComponent self, UnitType type)
        {
            self.UnitType = type;
        }
    }

    public class UnitTypeComponent : Entity
    {
        public UnitType UnitType; 
    }
}