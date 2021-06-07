using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class DOperaComponentAwakeSystem : AwakeSystem<DOperaComponent>
    {
        public override void Awake(DOperaComponent self)
        {
            self.mapMask = LayerMask.GetMask("Map");
            self.BeUpdate = false;

            self.MyGamer = null;

            self.SelectedTowers = new HashSet<DUnit>();
            self.TargetTower = null;

            self.OperaCampType = CampType.CampNone;
        }
    }

    public class DOperaComponentUpdateSystem : UpdateSystem<DOperaComponent>
    {
        public override void Update(DOperaComponent self)
        {
            self.Update();
        }
    }

    public static class DOperaComponentSystem
    {
        public static void Update(this DOperaComponent self)
        {
            self.InitMyGamer();

            // 拾取
            if (self.BeUpdate)
            {
                DUnit tower = OperaHelper.PickUpUnit("Tower");
                // 拾取对象
                self.SelectUnit(tower);
                // 设置之后的拾取对象
                self.SetTargetUnit(tower);
                // 刷新拾取MAP层的点
                self.UpdateClickPoint();
            }

            // 拾取状态
            if (Input.GetMouseButtonDown(0))
            {
                self.BeUpdate = true;
            }

            // 拾取状态
            if (Input.GetMouseButtonUp(0))
            {
                if (self.BeUpdate)
                {
                    // 触发操作检测事件
                    self.TrigerOperaEvent();

                    // 重置状态
                    self.ClearSelectState();
                }
            }
        }

        public static void InitMyGamer(this DOperaComponent self)
        {
            if (self.MyGamer == null)
            {
                Scene scene = self.GetParent<Scene>();
                self.MyGamer = scene.GetComponent<GamerComponent>().myGamer;
            }
        }

        public static void SelectUnit(this DOperaComponent self, DUnit unit)
        {
            // 如果拾取对象为空，直接返回
            if (unit == null)
            {
                return;
            }

            // 只可以拾取城堡类型的对象
            if (unit.GetComponent<UnitTypeComponent>().UnitType != UnitType.UnitTower)
            {
                return;
            }

            CampComponent camp = unit.GetComponent<CampComponent>();
            // 只有玩家自己控制的unit才可以被选中
            if (camp.CtrlGamerId != self.MyGamer.Id)
            {
                return;
            }

            // 设置当前操作的主动阵营
            if (self.SelectedTowers.Count == 0)
            {
                self.OperaCampType = camp.Camp;
            }

            // 当前操作主动阵营的，可以加入到拾取列表
            if (self.OperaCampType != camp.Camp)
            {
                return;
            }

            // 之前没有被拾取过，可以加入到拾取列表
            if (self.SelectedTowers.Contains(unit))
            {
                return;
            }
            self.SelectedTowers.Add(unit);
        }

        public static void SetTargetUnit(this DOperaComponent self, DUnit unit)
        {
            // 拾取对象为空，则TargetUnit标记为空
            if (unit == null)
            {
                self.TargetTower = null;
                return;
            }

            // 只可以拾取城堡类型的对象
            if (unit.GetComponent<UnitTypeComponent>().UnitType != UnitType.UnitTower)
            {
                self.TargetTower = null;
                return;
            }

            self.TargetTower = unit;
        }

        public static void UpdateClickPoint(this DOperaComponent self)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, self.mapMask))
            {
                self.ClickPoint = hit.point;
            }
        }

        public static void ClearSelectState(this DOperaComponent self)
        {
            self.BeUpdate = false;
            self.SelectedTowers.Clear();
            self.TargetTower = null;
            self.OperaCampType = CampType.CampNone;
        }

        public static void TrigerOperaEvent(this DOperaComponent self)
        {
            // 没有操作对象 || 没有目标对象
            if (self.SelectedTowers.Count == 0 || self.TargetTower == null)
            {
                return;
            }

            // 召唤角色
            foreach (var tower in self.SelectedTowers)
            {
                if (tower == self.TargetTower)
                {
                    continue;
                }

                int roleCount = TowerHelper.GetBattleRoleCount(tower);
                if (roleCount > 0)
                {
                    TowerHelper.SummonRoles(tower, self.MyGamer.Id, roleCount, self.TargetTower.Id);
                }
            }
        }
    }
}