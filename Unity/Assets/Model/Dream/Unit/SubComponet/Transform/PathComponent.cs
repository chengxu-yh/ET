using System.Threading;
using UnityEngine;

namespace ET
{
    public class PathComponent : Entity
    {
        public ListComponent<Vector3> Path;

        public Vector3 ServerPos;

        public ETCancellationToken CancellationTokenSource;
    }
}