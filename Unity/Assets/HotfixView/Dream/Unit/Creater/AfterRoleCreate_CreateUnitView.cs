﻿using UnityEngine;

namespace ET
{
    public class AfterRoleCreate_CreateUnitView : AEvent<AppEventType.AfterRoleCreate>
    {
        protected override async ETTask Run(AppEventType.AfterRoleCreate args)
        {
            CreateRoleViewAsync(args.Unit).Coroutine();

            await ETTask.CompletedTask;
        }

        private async ETTask CreateRoleViewAsync(DUnit role)
        {
            URoleConfig config = role.GetComponent<URoleConfigComponent>().RoleConfig;

            GameObject go = role.AddComponent<DGameObjectComponent>().Init("Role.unity3d", "Role", config.Model, GlobalComponent.Instance.Unit);
            go.AddComponent<ComponentView>().Component = role;
            go.transform.position = role.Position;

            // 声音组件
            role.AddComponent<SoundComponent>();
            // 动画组件
            role.AddComponent<DAnimatorComponent>();

            await ETTask.CompletedTask;
        }
    }
}