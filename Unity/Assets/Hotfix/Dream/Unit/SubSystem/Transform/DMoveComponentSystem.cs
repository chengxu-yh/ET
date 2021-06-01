using System;
using UnityEngine;

namespace ET
{
    [ObjectSystem]
    public class DMoveComponentAwakeSystem: AwakeSystem<DMoveComponent>
    {
        public override void Awake(DMoveComponent self)
        {
            self.StartTime = 0;
            self.Target = Vector3.zero;
            self.StartPos = Vector3.zero;
            self.needTime = 0;
            self.Callback = null;
        }
    }

    [ObjectSystem]
    public class DMoveComponentDestroySystem : DestroySystem<DMoveComponent>
    {
        public override void Destroy(DMoveComponent self)
        {
            self.StartTime = 0;
            self.Target = Vector3.zero;
            self.StartPos = Vector3.zero;
            self.needTime = 0;

            if (self.Callback != null)
            {
                Action<bool> callback = self.Callback;
                self.Callback = null;
                callback.Invoke(false);
            }
        }
    }

    [ObjectSystem]
    public class DMoveComponentUpdateSystem : UpdateSystem<DMoveComponent>
    {
        public override void Update(DMoveComponent self)
        {
            if (self.Callback == null)
            {
                return;
            }

            DUnit unit = self.GetParent<DUnit>();
            long timeNow = TimeHelper.ClientNow();

            if (timeNow - self.StartTime >= self.needTime)
            {
                unit.Position = self.Target;

                Action<bool> callback = self.Callback;
                self.Callback = null;
                callback.Invoke(true);

                return;
            }

            float amount = (timeNow - self.StartTime) * 1f / self.needTime;
            unit.Position = Vector3.Lerp(self.StartPos, self.Target, amount);
        }
    }

    public static class DMoveComponentSystem
    {
        public static async ETTask<bool> MoveToAsync(this DMoveComponent self, Vector3 target, float speedValue, ETCancellationToken cancellationToken = null)
        {
            await ETTask.CompletedTask;

            DUnit unit = self.GetParent<DUnit>();

            if ((target - self.Target).magnitude < 0.1f)
            {
                return true;
            }

            self.Target = target;


            self.StartPos = unit.Position;
            self.StartTime = TimeHelper.ClientNow();
            float distance = (self.Target - self.StartPos).magnitude;
            if (Math.Abs(distance) < 0.1f)
            {
                return true;
            }

            self.needTime = (long)(distance / speedValue * 1000);

            ETTask<bool> moveTcs = ETTask<bool>.Create();
            self.Callback = (ret) => { moveTcs.SetResult(ret); };

            void CancelAction()
            {
                if (self.Callback != null)
                {
                    Action<bool> callback = self.Callback;
                    self.Callback = null;
                    callback.Invoke(false);
                }
            }

            bool moveRet;
            try
            {
                cancellationToken?.Add(CancelAction);
                moveRet = await moveTcs;
            }
            finally
            {
                cancellationToken?.Remove(CancelAction);
            }

            return moveRet;
        }
    }
}