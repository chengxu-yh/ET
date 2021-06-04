namespace ET
{
    public class UTrapConfigComponentAwakeSystem : AwakeSystem<UTrapConfigComponent, int>
    {
        public override void Awake(UTrapConfigComponent self,int configid)
        {
            self.ConfigId = configid;
        }
    }

    public class UTrapConfigComponent : Entity
    {
        public int ConfigId; //配置表id

        public UTrapConfig TrapConfig => UTrapConfigCategory.Instance.Get(this.ConfigId);

    }
}