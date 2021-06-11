namespace ET
{
    public class UnitStateComponentAwakeSystem : AwakeSystem<UnitStateComponent, int>
    {
        public override void Awake(UnitStateComponent self, int state)
        {
            self.UnitState = state;
        }
    }

    public class UnitStateComponent : Entity
    {
        public int UnitState; 
    }
}