namespace ET
{
    public static class DUnitFactory
    {
        public static DUnit Create(Entity domain)
        {
	        DUnit unit = EntityFactory.Create<DUnit>(domain);
	        unit.AddComponent<ObjectWait>();

	        DUnitComponent unitComponent = domain.GetComponent<DUnitComponent>();
            unitComponent.Add(unit);

            return unit;
        }
    }
}
