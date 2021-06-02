using System;
using UnityEngine;

namespace ET
{
    [ObjectSystem]
    public class PathComponentAwakeSystem : AwakeSystem<PathComponent>
    {
        public override void Awake(PathComponent self)
        {
            self.Path = ListComponent<Vector3>.Create();
            self.ServerPos = Vector3.zero;
            self.CancellationTokenSource = null;
        }
    }

    [ObjectSystem]
    public class PathComponentDestroySystem : DestroySystem<PathComponent>
    {
        public override void Destroy(PathComponent self)
        {
            self.Path = null;
            self.ServerPos = Vector3.zero;
            self.CancellationTokenSource = null;
        }
    }

    public static class PathComponentSystem
    {
        public static async ETTask<bool> StartMoveImp(this PathComponent self, ETCancellationToken cancellationToken)
        {
            DUnit unit = self.GetParent<DUnit>();
            float speed = unit.GetComponent<NumericComponent>().GetAsFloat(NumericType.Speed);

            for (int i = 0; i < self.Path.List.Count; ++i)
            {
                Vector3 v = self.Path.List[i];

                if (i == 0)
                {
                    float serverf = (self.ServerPos - v).magnitude;
                    if (serverf > 0.1f)
                    {
                        float clientf = (unit.Position - v).magnitude;
                        speed = clientf / serverf * speed;
                    }
                }

                unit.GetComponent<TurnComponent>().Turn(v);

                if (await unit.GetComponent<DMoveComponent>().MoveToAsync(v, speed, cancellationToken) == false)
                {
                    return false;
                }
            }
            return true;
        }

        public static async ETTask<bool> StartMove(this PathComponent self, Vector3[] paths, Vector3 servierpos, ETCancellationToken cancellationToken = null)
        {
            self.Path.List.Clear();

            // 取消之前的移动协程
            self.CancellationTokenSource?.Cancel();

            if (cancellationToken == null)
            {
                self.CancellationTokenSource = new ETCancellationToken();
            }
            else
            {
                self.CancellationTokenSource = cancellationToken;
            }
            
            // 新数据初始化
            for (int i = 0; i < paths.Length; ++i)
            {
                self.Path.List.Add(paths[i]);
            }
            self.ServerPos = servierpos;

            await Game.EventSystem.Publish(new AppEventType.MoveStart() { Unit = self.GetParent<DUnit>() });

            bool res = await self.StartMoveImp(self.CancellationTokenSource);

            await Game.EventSystem.Publish(new AppEventType.MoveStop() { Unit = self.GetParent<DUnit>() });

            self.CancellationTokenSource = null;

            return res;
        }
    }
}