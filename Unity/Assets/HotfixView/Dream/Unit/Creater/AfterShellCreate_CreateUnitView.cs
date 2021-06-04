using UnityEngine;

namespace ET
{
    public class AfterShellCreate_CreateUnitView : AEvent<AppEventType.AfterShellCreate>
    {
        protected override async ETTask Run(AppEventType.AfterShellCreate args)
        {
            CreateShellViewAsync(args.Unit).Coroutine();

            await ETTask.CompletedTask;
        }

        private async ETTask CreateShellViewAsync(DUnit shell)
        {
            UShellConfig config = shell.GetComponent<UShellConfigComponent>().ShellConfig;

            GameObject go = shell.AddComponent<DGameObjectComponent>().Init("Shell.unity3d", "Shell", config.Model, GlobalComponent.Instance.Unit);
            go.AddComponent<ComponentView>().Component = shell;
            go.transform.position = shell.Position;

            await ETTask.CompletedTask;
        }
    }
}