using UnityEngine;

namespace ET
{
    public static class OperaHelper
    {
        public static DUnit PickUpUnit(string layer)
        {
            int layerMask = LayerMask.GetMask(layer);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, layerMask))
            {
                ComponentView unitview = hit.collider.gameObject.GetComponent<ComponentView>();
                if (unitview == null)
                {
                    unitview = hit.transform.parent.gameObject.GetComponent<ComponentView>();
                }

                if (unitview != null)
                {
                    DUnit unit = unitview.Component as DUnit;
                    return unit;
                }
            }

            return null;
        }
    }
}
 