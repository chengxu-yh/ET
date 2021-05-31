using System;
using UnityEngine;

namespace ET
{
    public class DOperaComponentAwakeSystem : AwakeSystem<DOperaComponent>
    {
        public override void Awake(DOperaComponent self)
        {
            self.mapMask = LayerMask.GetMask("Map");
            self.BeUpdate = false;
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
            if (self.BeUpdate)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, self.mapMask))
                {
                    self.ClickPoint = hit.point;
                }
            }
        }

        
    }
}