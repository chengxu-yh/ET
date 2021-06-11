namespace ET
{
    public static class TowerHelper
    {
        public static void InitTowerNumberic(DUnit tower)
        {
            NumericComponent numeric = tower.AddComponent<NumericComponent>();
            UTowerConfig config = tower.GetComponent<UTowerConfigComponent>().TowerConfig;

            // 最大血量
            numeric.Set(NumericType.MaxHpBase, config.MaxHP);
            // 血量
            numeric.Set(NumericType.HpBase, config.HP);
            // 攻速
            numeric.Set(NumericType.AttackSpeedBase, config.AttackSpeed);
            // 血量恢复
            numeric.Set(NumericType.HPRegainBase, config.HPRegain);
            // 攻击力
            numeric.Set(NumericType.HPDamageBase, config.HPDamage);
        }

        public static int GetBattleRoleCount(DUnit tower)
        {
            NumericComponent numeric = tower.GetComponent<NumericComponent>();
            int halfHpAdd = (numeric.GetAsInt(NumericType.Hp) - numeric.GetAsInt(NumericType.HpBase)) / 2;
            if (halfHpAdd <= 0)
            {
                return 0;
            }

            int RoleId = tower.GetComponent<UTowerConfigComponent>().TowerConfig.RoleId;
            URoleConfig RoleConfig = URoleConfigCategory.Instance.Get(RoleId);
            if (RoleConfig == null)
            {
                return 0;
            }

            return halfHpAdd / RoleConfig.HP;
        }

        public static void SummonRoles(DUnit tower, long gamerid, int count, long targetid)
        {
            // 自身血量检测
            NumericComponent numeric = tower.GetComponent<NumericComponent>();
            int hpAdd = (numeric.GetAsInt(NumericType.Hp) - numeric.GetAsInt(NumericType.HpBase));
            if (hpAdd <= 0)
            {
                return;
            }

            // 召唤数量检测
            int roleConfigId = tower.GetComponent<UTowerConfigComponent>().TowerConfig.RoleId;
            int roleHp = URoleConfigCategory.Instance.Get(roleConfigId).HP;
            if (roleHp * count > hpAdd)
            {
                count = hpAdd / roleHp;
            }

            // 包装召唤ROLE
            DUnitInfo roleinfo = new DUnitInfo();
            roleinfo.GamerId = gamerid;
            roleinfo.UnitId = IdGenerater.Instance.GenerateUnitId(0);
            roleinfo.ConfigId = roleConfigId;
            roleinfo.Camp = (int)(tower.GetComponent<CampComponent>().Camp);
            roleinfo.PX = tower.Position.x;
            roleinfo.PY = tower.Position.y;
            roleinfo.PZ = tower.Position.z;
            roleinfo.RX = tower.Rotation.x;
            roleinfo.RY = tower.Rotation.y;
            roleinfo.RZ = tower.Rotation.z;
            roleinfo.RW = tower.Rotation.w;
            roleinfo.Count = count;
            roleinfo.TargetId = targetid;
            roleinfo.OperationerId = gamerid;

            RoleFactory.Create(tower.Domain, roleinfo);

        }
    }
}
