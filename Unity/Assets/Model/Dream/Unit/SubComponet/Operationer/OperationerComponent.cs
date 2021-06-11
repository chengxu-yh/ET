namespace ET
{
    public class OperationerComponentAwakeSystem : AwakeSystem<OperationerComponent, long>
    {
        public override void Awake(OperationerComponent self,long operid)
        {
            self.OperationerId = operid;
        }
    }

    public class OperationerComponent : Entity
    {
        public long OperationerId { get; set; } 
    }
}