namespace ET
{
    public class UTowerConfigComponentAwakeSystem : AwakeSystem<UTowerConfigComponent, int>
    {
        public override void Awake(UTowerConfigComponent self,int configid)
        {
            self.ConfigId = configid;
        }
    }

    public class UTowerConfigComponent : Entity
    {
        public int ConfigId; //配置表id

        public UTowerConfig TowerConfig => UTowerConfigCategory.Instance.Get(this.ConfigId);

    }
}