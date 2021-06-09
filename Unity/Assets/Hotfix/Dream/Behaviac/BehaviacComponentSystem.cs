namespace ET
{
    public class BehaviacComponentAwakeSystem: AwakeSystem<BehaviacComponent>
    {
        public override void Awake(BehaviacComponent self)
        {
            behaviac.Workspace.Instance.FileFormat = behaviac.Workspace.EFileFormat.EFF_cs;
        }
    }

    public class BehaviacComponentDestroySystem : DestroySystem<BehaviacComponent>
    {
        public override void Destroy(BehaviacComponent self)
        {
            behaviac.Workspace.Instance.Cleanup();
        }
    }

}