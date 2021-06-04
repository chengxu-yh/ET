namespace ET
{
    /// <summary>
    /// 监视hp数值变化，改变血条值
    /// </summary>
    [NumericWatcher(NumericType.HPRegain)]
    public class NumericWatcher_HPRegain : INumericWatcher
    {
        public void Run(EventType.NumbericChange args)
        {
            DUnit tower = args.Parent as DUnit;
            if (tower != null)
            {
                HPRegainComponent hPRegainComponent = tower.GetComponent<HPRegainComponent>();
                if (hPRegainComponent != null)
                {
                    hPRegainComponent.HpRegain = (int)args.New;
                }
            }
        }
    }
}
