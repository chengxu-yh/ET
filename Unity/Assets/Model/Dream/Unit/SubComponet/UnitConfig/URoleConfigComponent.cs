namespace ET
{
    public class UnitConfigComponentAwakeSystem : AwakeSystem<URoleConfigComponent, int>
    {
        public override void Awake(URoleConfigComponent self,int configid)
        {
            self.ConfigId = configid;
        }
    }

    public class URoleConfigComponent : Entity
    {
        public int ConfigId; //配置表id

        public URoleConfig RoleConfig => URoleConfigCategory.Instance.Get(this.ConfigId);

    }
}