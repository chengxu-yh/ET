namespace ET
{
    public class UShellConfigComponentAwakeSystem : AwakeSystem<UShellConfigComponent, int>
    {
        public override void Awake(UShellConfigComponent self,int configid)
        {
            self.ConfigId = configid;
        }
    }

    public class UShellConfigComponent : Entity
    {
        public int ConfigId; //配置表id

        public UShellConfig RoleConfig => UShellConfigCategory.Instance.Get(this.ConfigId);


    }
}