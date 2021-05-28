namespace ET
{
    public static class DUnitFactory
    {
        public static DUnit Create(Entity domain, long unitid)
        {
            DUnit unit = EntityFactory.CreateWithId<DUnit>(domain, unitid);
	        unit.AddComponent<ObjectWait>();

	        DUnitComponent unitComponent = domain.GetComponent<DUnitComponent>();
            unitComponent.Add(unit);

            return unit;
        }
    }
}
