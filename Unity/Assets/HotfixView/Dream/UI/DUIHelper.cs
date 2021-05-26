using UnityEngine;

namespace ET
{
    public static class DUIHelper
    {
        public static async ETTask<UI> Create(Scene scene, string uiType)
        {
            return await scene.GetComponent<UIComponent>().Create(uiType);
        }
        
        public static async ETTask Remove(Scene scene, string uiType)
        {
            scene.GetComponent<UIComponent>().Remove(uiType);
            await ETTask.CompletedTask;
        }

        public static async ETTask<UI> InstantiateFromBundle(UIComponent uiComponent, string uiBundle)
        {
            await ResourcesComponent.Instance.LoadBundleAsync(uiBundle.StringToAB());

            GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset(uiBundle.StringToAB(), uiBundle);
            GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);

            UI ui = EntityFactory.CreateWithParent<UI, string, GameObject>(uiComponent, uiBundle, gameObject);

            return ui;
        }

        public static void UnloadBundle(string uiBundle)
        {
            ResourcesComponent.Instance.UnloadBundle(uiBundle.StringToAB());
        }
    }
}