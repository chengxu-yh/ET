using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class DOperaComponent : Entity
    {
        // 操作界面管理的GAMER
        public Gamer MyGamer;

        //拖拽过程中，实时更新的拖拽点
        public Vector3 ClickPoint;

        // 控制是否刷新拖拽点
        public bool BeUpdate;

        // 拖拽点拾取层级
        public int mapMask;

        // 选中的Units
        public HashSet<DUnit> SelectedTowers;

        // 最后一个拾取的Unit
        public DUnit TargetTower;

        // 当前操作阵营
        public CampType OperaCampType;
    }
}
