using UnityEngine;

namespace ET
{
    public class DGameObjectComponentDestroySystem : DestroySystem<DGameObjectComponent>
    {
        public override void Destroy(DGameObjectComponent self)
        {
            GameObjectPool.Instance.Recycle(self.GameObject);

            self.GameObject = null;
        }
    }

    public static class DGameObjectComponentSystem
    {
        public static GameObject Init(this DGameObjectComponent self, string bundle, string variant, string name, Transform parent = null)
        {
            GameObject gameobject = GameObjectPool.Instance.Fetch(bundle, variant, name, parent);

            self.GameObject = gameobject;

            return gameobject;
        }
    }
}
