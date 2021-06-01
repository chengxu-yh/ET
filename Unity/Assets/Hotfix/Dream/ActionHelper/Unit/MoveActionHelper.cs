using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ET
{
    public static class DMoveAction
    {
        public static async ETTask<int> MoveActionAsync(this DUnit unit, Vector3 targetPos, ETCancellationToken cancellationToken = null)
        {
            NavMeshPath meshPath = new NavMeshPath();

            NavMesh.CalculatePath(unit.Position, targetPos, 1, meshPath);

            if (unit.DomainScene().GetComponent<PVPComponent>().bePVP)
            {
                // PVP 发送移动消息，封包发送
                C2M_DPathfindingResult msg = new C2M_DPathfindingResult();
                msg.Id = unit.Id;
                msg.X = unit.Position.x;
                msg.Y = unit.Position.y;
                msg.Z = unit.Position.z;
                for (int i = 0; i < meshPath.corners.Length; i++)
                {
                    msg.Xs.Add(meshPath.corners[i].x);
                    msg.Ys.Add(meshPath.corners[i].y);
                    msg.Zs.Add(meshPath.corners[i].z);
                }
                Debug.Log("move start");
                unit.Domain.GetComponent<SessionComponent>().Session.Send(msg);

                ObjectWait objectWait = unit.GetComponent<ObjectWait>();
                objectWait.Notify(new WaitType.Wait_UnitStop() { Error = WaitTypeError.Cancel });

                WaitType.Wait_UnitStop waitUnitStop = await objectWait.Wait<WaitType.Wait_UnitStop>(cancellationToken);
                Debug.Log("move End");
                return waitUnitStop.Error;
            }
            else
            {
                return await unit.MoveActionImpAsync(meshPath.corners, unit.Position, cancellationToken);
            }
        }

        public static async ETTask<int> MoveActionImpAsync(this DUnit unit, Vector3[] paths, Vector3 serverpos, ETCancellationToken cancellationToken = null)
        {
            PathComponent pathComponent = unit.GetComponent<PathComponent>();
            if (await pathComponent.StartMove(paths, serverpos, cancellationToken))
            {
                ObjectWait objectWait = unit.GetComponent<ObjectWait>();
                objectWait.Notify(new WaitType.Wait_UnitStop() { Error = WaitTypeError.Success });

                return WaitTypeError.Success;
            }
            else
            {
                return WaitTypeError.Cancel;
            }
        }

    }
}