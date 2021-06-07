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

            // ʰȡ
            if (self.BeUpdate)
            {
                DUnit tower = OperaHelper.PickUpUnit("Tower");
                // ʰȡ����
                self.SelectUnit(tower);
                // ����֮���ʰȡ����
                self.SetTargetUnit(tower);
                // ˢ��ʰȡMAP��ĵ�
                self.UpdateClickPoint();
            }

            // ʰȡ״̬
            if (Input.GetMouseButtonDown(0))
            {
                self.BeUpdate = true;
            }

            // ʰȡ״̬
            if (Input.GetMouseButtonUp(0))
            {
                if (self.BeUpdate)
                {
                    // ������������¼�
                    self.TrigerOperaEvent();

                    // ����״̬
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
            // ���ʰȡ����Ϊ�գ�ֱ�ӷ���
            if (unit == null)
            {
                return;
            }

            // ֻ����ʰȡ�Ǳ����͵Ķ���
            if (unit.GetComponent<UnitTypeComponent>().UnitType != UnitType.UnitTower)
            {
                return;
            }

            CampComponent camp = unit.GetComponent<CampComponent>();
            // ֻ������Լ����Ƶ�unit�ſ��Ա�ѡ��
            if (camp.CtrlGamerId != self.MyGamer.Id)
            {
                return;
            }

            // ���õ�ǰ������������Ӫ
            if (self.SelectedTowers.Count == 0)
            {
                self.OperaCampType = camp.Camp;
            }

            // ��ǰ����������Ӫ�ģ����Լ��뵽ʰȡ�б�
            if (self.OperaCampType != camp.Camp)
            {
                return;
            }

            // ֮ǰû�б�ʰȡ�������Լ��뵽ʰȡ�б�
            if (self.SelectedTowers.Contains(unit))
            {
                return;
            }
            self.SelectedTowers.Add(unit);
        }

        public static void SetTargetUnit(this DOperaComponent self, DUnit unit)
        {
            // ʰȡ����Ϊ�գ���TargetUnit���Ϊ��
            if (unit == null)
            {
                self.TargetTower = null;
                return;
            }

            // ֻ����ʰȡ�Ǳ����͵Ķ���
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
            // û�в������� || û��Ŀ�����
            if (self.SelectedTowers.Count == 0 || self.TargetTower == null)
            {
                return;
            }

            // �ٻ���ɫ
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