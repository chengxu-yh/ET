namespace ET
{
	/// <summary>
	/// 监视hp数值变化，改变血条值
	/// </summary>
	[NumericWatcher(NumericType.AttackSpeed)]
	public class NumericWatcher_AttackSpeed : INumericWatcher
	{
		public void Run(long id, long value)
		{
		}
	}
}
