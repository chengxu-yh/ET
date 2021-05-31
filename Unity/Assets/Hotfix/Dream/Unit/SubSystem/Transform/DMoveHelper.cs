using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public static class DMoveHelper
    {
        // 可以多次调用，多次调用的话会取消上一次的协程
        public static async ETTask<int> MoveToAsync(this DUnit unit, Vector3 targetPos, ETCancellationToken cancellationToken = null)
        {
            // 客户端计算寻路信息，并将结果广播给服务器

            ObjectWait objectWait = unit.GetComponent<ObjectWait>();
            // 要取消上一次的移动协程
            objectWait.Notify(new WaitType.Wait_UnitStop() { Error = WaitTypeError.Cancel });

            // 一直等到unit发送stop
            WaitType.Wait_UnitStop waitUnitStop = await objectWait.Wait<WaitType.Wait_UnitStop>(cancellationToken);
            return waitUnitStop.Error;
        }
    }
}