namespace ET
{
    public class PVPComponentAwakeSystem: AwakeSystem<PVPComponent, bool>
    {
        public override void Awake(PVPComponent self, bool bepvp)
        {
            self.bePVP = bepvp;
        }
    }
}