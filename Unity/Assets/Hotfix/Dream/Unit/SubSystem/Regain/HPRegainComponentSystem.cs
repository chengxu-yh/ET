using System;
using UnityEngine;

namespace ET
{
    public class HPRegainComponentAwakeSystem : AwakeSystem<HPRegainComponent, int>
    {
        public override void Awake(HPRegainComponent self, int hpregain)
        {
            self.HpRegain = hpregain;

            self.BeStart = true;
            self.StartMilliseconds = TimeHelper.ClientNow();
        }
    }

    public class HPRegainComponentUpdateSystem : UpdateSystem<HPRegainComponent>
    {
        public override void Update(HPRegainComponent self)
        {
            if (self.BeStart)
            {
                // 按秒恢复
                long disTime = TimeHelper.ClientNow() - self.StartMilliseconds;
                if (disTime > 1000)
                {
                    NumericComponent nm = self.GetParent<DUnit>().GetComponent<NumericComponent>();
                    int hpMax = nm.GetAsInt(NumericType.MaxHp);
                    int hp = nm.GetAsInt(NumericType.Hp);
                    // 到最大值，不再恢复
                    if (hp >= hpMax)
                    {
                        return;
                    }

                    // 确认恢复值
                    int hpRegain = ((int)disTime / 1000) * self.HpRegain;
                    if (hp + hpRegain > hpMax)
                    {
                        hpRegain = hpMax - hp;
                    }

                    // 设置增量值
                    int hpAdd = nm.GetAsInt(NumericType.HpAdd) + hpRegain;
                    nm.Set(NumericType.HpAdd, hpAdd);

                    int extTime = (int)disTime % 1000;
                    self.StartMilliseconds = TimeHelper.ClientNow() - extTime;
                }
                return;
            }
        }
    }

    public static class HPRegainComponentSystem
    {
        public static void StartRegainHp(this HPRegainComponent self)
        {
            self.BeStart = true;
            self.StartMilliseconds = TimeHelper.ClientNow();
        }
    }
}