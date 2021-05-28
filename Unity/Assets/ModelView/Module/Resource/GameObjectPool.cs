using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class GameObjectQueue: Object
    {
        public string TypeName
        {
            get;
        }

        private readonly Queue<GameObject> queue = new Queue<GameObject>();

        public GameObjectQueue(string typeName)
        {
            this.TypeName = typeName;
        }

        public void Enqueue(GameObject entity)
        {
            this.queue.Enqueue(entity);
        }

        public GameObject Dequeue()
        {
            return this.queue.Dequeue();
        }

        public GameObject Peek()
        {
            return this.queue.Peek();
        }

        public Queue<GameObject> Queue => this.queue;

        public int Count => this.queue.Count;

        public override void Dispose()
        {
            while (this.queue.Count > 0)
            {
                GameObject gameobject = this.queue.Dequeue();
                UnityEngine.Object.Destroy(gameobject);
            }
        }
    }

    public class GameObjectPool: Object
    {
        private static GameObjectPool instance;

        public static GameObjectPool Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObjectPool();
                }

                return instance;
            }
        }

        private GameObject GameObjPool;

        GameObjectPool()
        {
            this.GameObjPool = new GameObject("GameObjPool");
            this.GameObjPool.SetActive(false);
            GameObject.DontDestroyOnLoad(this.GameObjPool);
        }

        public readonly Dictionary<string, GameObjectQueue> dictionary = new Dictionary<string, GameObjectQueue>();

        public GameObject Fetch(string bundle, string variant, string name, string type, Transform parent = null)
        {
            GameObject obj;
            if (!this.dictionary.TryGetValue(type, out GameObjectQueue queue))
            {
                obj = GameObjectHelper.CreateInstance(bundle, variant, name, parent);
            }
            else if (queue.Count == 0)
            {
                obj = GameObjectHelper.CreateInstance(bundle, variant, name, parent);
            }
            else
            {
                obj = queue.Dequeue();
            }

            obj.transform.parent = parent;
            return obj;
        }


        public void Recycle(GameObject obj, string type)
        {
            GameObjectQueue queue;
            if (!this.dictionary.TryGetValue(type, out queue))
            {
                queue = new GameObjectQueue(type);

#if UNITY_EDITOR && VIEWGO
                if (queue.ViewGO != null)
                {
                    queue.ViewGO.transform.SetParent(this.ViewGO.transform);
                    queue.ViewGO.name = $"{type.Name}s";
                }
#endif
                this.dictionary.Add(type, queue);
            }

#if UNITY_EDITOR && VIEWGO
            if (obj.ViewGO != null)
            {
                obj.ViewGO.transform.SetParent(queue.ViewGO.transform);
            }
#endif
            obj.transform.parent = this.GameObjPool.transform;
            queue.Enqueue(obj);
        }

        public void Clear()
        {
            foreach (KeyValuePair<string, GameObjectQueue> kv in this.dictionary)
            {
                kv.Value.Dispose();
            }

            this.dictionary.Clear();
        }

        public override void Dispose()
        {
            foreach (KeyValuePair<string, GameObjectQueue> kv in this.dictionary)
            {
                kv.Value.Dispose();
            }

            this.dictionary.Clear();
            instance = null;
        }
    }
}