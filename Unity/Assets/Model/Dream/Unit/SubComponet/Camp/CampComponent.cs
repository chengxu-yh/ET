namespace ET
{
    public class CampComponentAwakeSystem : AwakeSystem<CampComponent, long, int>
    {
        public override void Awake(CampComponent self,long gamerid, int camp)
        {
            self.CtrlGamerId = gamerid;
            self.Camp = camp;
        }
    }

    public class CampComponent : Entity
    {
        public long CtrlGamerId { get; set; } 

        public int Camp { get; set; }
    }
}