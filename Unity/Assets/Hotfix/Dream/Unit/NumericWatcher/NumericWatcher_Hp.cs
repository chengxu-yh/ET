﻿namespace ET
{
	/// <summary>
	/// 监视hp数值变化，改变血条值
	/// </summary>
	[NumericWatcher(NumericType.Hp)]
	public class NumericWatcher_Hp : INumericWatcher
	{
		public void Run(long id, long value)
		{
		}
	}
}
