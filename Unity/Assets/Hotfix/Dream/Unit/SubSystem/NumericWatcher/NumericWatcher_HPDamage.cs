namespace ET
{
	/// <summary>
	/// 监视hp数值变化，改变血条值
	/// </summary>
	[NumericWatcher(NumericType.HPDamage)]
	public class NumericWatcher_HPDamage : INumericWatcher
	{
		public void Run(EventType.NumbericChange args)
		{
		}
	}
}
