namespace ET
{
    public static class ShellHelper
    {
        public static void InitShellNumberic(DUnit shell)
        {
            NumericComponent numeric = shell.AddComponent<NumericComponent>();
            UShellConfig config = shell.GetComponent<UShellConfigComponent>().ShellConfig;

            // 速度
            numeric.Set(NumericType.SpeedBase, config.MoveSpeed);
        }
    }
}
