using System.Linq;

namespace ET
{

    public class DUnitComponentAwakeSystem : AwakeSystem<DUnitComponent>
	{
		public override void Awake(DUnitComponent self)
		{
		}
	}
	
	public class DUnitComponentDestroySystem : DestroySystem<DUnitComponent>
	{
		public override void Destroy(DUnitComponent self)
		{
			foreach (DUnit unit in self.idUnits.Values)
			{
				unit.Dispose();
			}

			self.idUnits.Clear();
		}
	}
	
	public static class DUnitComponentSystem
	{
		public static void Add(this DUnitComponent self, DUnit unit)
		{
			self.idUnits.Add(unit.Id, unit);
			unit.Parent = self;
		}

		public static DUnit Get(this DUnitComponent self, long id)
		{
			DUnit unit;
			self.idUnits.TryGetValue(id, out unit);
			return unit;
		}

		public static void Remove(this DUnitComponent self, long id)
		{
			DUnit unit;
			self.idUnits.TryGetValue(id, out unit);
			self.idUnits.Remove(id);
			unit?.Dispose();
		}

		public static void RemoveNoDispose(this DUnitComponent self, long id)
		{
			self.idUnits.Remove(id);
		}

		public static DUnit[] GetAll(this DUnitComponent self)
		{
			return self.idUnits.Values.ToArray();
		}
	}
}