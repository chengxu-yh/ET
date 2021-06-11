namespace ET
{
    public class URoleConfigComponentAwakeSystem : AwakeSystem<URoleConfigComponent, int>
    {
        public override void Awake(URoleConfigComponent self,int configid)
        {
            self.ConfigId = configid;
            self.RoleType = UnitRoleTypeHelper.GetRoleType(self.RoleConfig.RoleType);
        }
    }

    public class URoleConfigComponent : Entity
    {
        public int ConfigId; //配置表id

        public UnitRoleType RoleType;   // 角色类型

        public URoleConfig RoleConfig => URoleConfigCategory.Instance.Get(this.ConfigId);

    }
}