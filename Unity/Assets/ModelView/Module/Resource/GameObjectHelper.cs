using System;
using UnityEngine;

namespace ET
{
    public static class GameObjectHelper
    {
        public static T Ensure<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject)
            {
                var t = gameObject.GetComponent<T>();

                if (!t)
                {
                    t = gameObject.AddComponent<T>();
                }

                return t;
            }

            return null;
        }

        public static T Ensure<T>(this Component component) where T : Component
        {
            if (component)
            {
                return Ensure<T>(component.gameObject);
            }

            return null;
        }

        public static T Get<T>(this GameObject gameObject, string key) where T : class
        {
            try
            {
                return gameObject.GetComponent<ReferenceCollector>().Get<T>(key);
            }
            catch (Exception e)
            {
                throw new Exception($"获取{gameObject.name}的ReferenceCollector key失败, key: {key}", e);
            }
        }

        public static GameObject CreateInstance(string bundle, string variant, string name, Transform parent)
        {
            GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset(bundle, variant);

            GameObject prefab = bundleGameObject.Get<GameObject>(name);

            GameObject go = UnityEngine.Object.Instantiate(prefab, parent, true);

            return go;
        }
    }
}