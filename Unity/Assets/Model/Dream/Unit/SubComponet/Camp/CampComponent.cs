namespace ET
{
    public class CampComponentAwakeSystem : AwakeSystem<CampComponent, long, CampType>
    {
        public override void Awake(CampComponent self,long gamerid, CampType camp)
        {
            self.CtrlGamerId = gamerid;
            self.Camp = camp;
        }
    }

    public class CampComponent : Entity
    {
        public long CtrlGamerId { get; set; } 

        public CampType Camp { get; set; }
    }
}