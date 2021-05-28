using UnityEngine;

namespace ET
{
    public class DGameObjectComponentDestroySystem : DestroySystem<DGameObjectComponent>
    {
        public override void Destroy(DGameObjectComponent self)
        {
            GameObjectPool.Instance.Recycle(self.GameObject, self.type);

            self.GameObject = null;
            self.type = null;
        }
    }

    public static class DGameObjectComponentSystem
    {
        public static GameObject Init(this DGameObjectComponent self, string bundle, string variant, string name, Transform parent = null)
        {
            self.type = bundle + variant + name;

            GameObject gameobject = GameObjectPool.Instance.Fetch(bundle, variant, name, self.type, parent);

            self.GameObject = gameobject;

            return gameobject;
        }


    }
}
