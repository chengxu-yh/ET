namespace ET
{
    public static class DUnitFactory
    {
        public static DUnit Create(Entity domain, long unitid)
        {
            DUnit unit = EntityFactory.CreateWithId<DUnit>(domain, unitid);
	        unit.AddComponent<ObjectWait>();
            unit.AddComponent<UnitStateComponent, int>((int)UnitState.Born);

            DUnitComponent unitComponent = domain.GetComponent<DUnitComponent>();
            unitComponent.Add(unit);

            return unit;
        }
    }
}
