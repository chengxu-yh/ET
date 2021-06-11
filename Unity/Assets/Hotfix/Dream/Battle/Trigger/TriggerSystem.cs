namespace ET
{
    public class TriggerAwakeSystem : AwakeSystem<Trigger, TriggerType>
    {
        public override void Awake(Trigger self, TriggerType type)
        {
            self.TriggerType = type;
            self.UnitList = ListComponent<long>.Create();
        }
    }

    public class TriggerDestroySystem : DestroySystem<Trigger>
    {
        public override void Destroy(Trigger self)
        {
            self.UnitList.List.Clear();
            self.UnitList = null;
        }
    }

    public static class TriggerSystem
    {
        public static void UpdateUnitList(this Trigger self)
        {
            ListComponent<long> deleteList = ListComponent<long>.Create();
            DUnitComponent dUnitComponent = self.Domain.GetComponent<DUnitComponent>();

            for (int i = 0; i < self.UnitList.List.Count; i++)
            {
                DUnit unit = dUnitComponent.Get(self.UnitList.List[i]);
                if (unit == null)
                {
                    deleteList.List.Add(unit.Id);
                    continue;
                }

                if (unit.GetComponent<UnitStateComponent>().UnitState == (int)UnitState.Death)
                {
                    deleteList.List.Add(unit.Id);
                }
            }

            for (int i = 0; i < deleteList.List.Count; i++)
            {
                self.UnitList.List.Remove(deleteList.List[i]);
            }
            deleteList.List.Clear();
        }
    }

}