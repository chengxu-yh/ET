﻿namespace ET
{
	/// <summary>
	/// 监视hp数值变化，改变血条值
	/// </summary>
	[NumericWatcher(NumericType.AngerRegain)]
	public class NumericWatcher_AngerRegain : INumericWatcher
	{
		public void Run(long id, long value)
		{
		}
	}
}