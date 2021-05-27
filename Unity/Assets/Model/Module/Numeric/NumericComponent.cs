using System.Collections.Generic;

namespace ET
{
	namespace EventType
	{
		public struct NumbericChange
		{
			public Entity Parent;
			public NumericType NumericType;
			public long Old;
			public long New;
		}
	}
	
	
	public class NumericComponentAwakeSystem : AwakeSystem<NumericComponent>
	{
		public override void Awake(NumericComponent self)
		{
			self.Awake();
		}
	}

	public class NumericComponent: Entity
	{
		public Dictionary<int, long> NumericDic = new Dictionary<int, long>();

		public void Awake()
		{
			// 这里初始化base值
		}

		public float GetAsFloat(NumericType numericType)
		{
			return (float)GetByKey((int)numericType) / 10000;
		}
		
		public float GetAsFloat(int numericType)
		{
			return (float)GetByKey(numericType) / 10000;
		}

		public int GetAsInt(NumericType numericType)
		{
			return (int)GetByKey((int)numericType);
		}
		
		public long GetAsLong(NumericType numericType)
		{
			return GetByKey((int)numericType);
		}
		
		public int GetAsInt(int numericType)
		{
			return (int)GetByKey(numericType);
		}
		
		public long GetAsLong(int numericType)
		{
			return GetByKey(numericType);
		}

		public void Set(NumericType nt, float value)
		{
			this[nt] = (int) (value * 10000);
		}

		public void Set(NumericType nt, int value)
		{
			this[nt] = value;
		}
		
		public void Set(NumericType nt, long value)
		{
			this[nt] = value;
		}

		public long this[NumericType numericType]
		{
			get
			{
				return this.GetByKey((int) numericType);
			}
			set
			{
				long v = this.GetByKey((int) numericType);
				if (v == value)
				{
					return;
				}

				NumericDic[(int)numericType] = value;

				Update(numericType);
			}
		}

		private long GetByKey(int key)
		{
			long value = 0;
			this.NumericDic.TryGetValue(key, out value);
			return value;
		}

		public void Update(NumericType numericType)
		{
			if (numericType < NumericType.Max)
			{
				return;
			}
			int final = (int) numericType / 10;
			int bas = final * 10 + 1; 
			int add = final * 10 + 2;
			int pct = final * 10 + 3;

			long old = this.NumericDic[final];
			long result = (long)( (this.GetByKey(bas) + this.GetByKey(add)) * (1 + this.GetAsFloat(pct)));

			this.NumericDic[final] = result;
			Game.EventSystem.Publish(new EventType.NumbericChange()
			{
				Parent = this.Parent, 
				NumericType = (NumericType) final,
				Old = old,
				New = result
			}).Coroutine();
		}
	}
}