namespace ET
{
	/// <summary>
	/// 监视hp数值变化，改变血条值
	/// </summary>
	[NumericWatcher(NumericType.MaxHp)]
	public class NumericWatcher_MaxHp : INumericWatcher
	{
		public void Run(EventType.NumbericChange args)
		{
		}
	}
}
