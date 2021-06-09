using UnityEngine;

namespace ET
{
    public class AfterSkillCreate_CreateSkillView : AEvent<AppEventType.AfterSkillCreate>
    {
        protected override async ETTask Run(AppEventType.AfterSkillCreate args)
        {
            CreateSkillViewAsync(args.Skill).Coroutine();

            await ETTask.CompletedTask;
        }

        private async ETTask CreateSkillViewAsync(Skill skill)
        {
            // 创建技能触发器系统
            SkillTriggerHelper.CreateSkillTrigger(skill);

            // 技能释放范围缩放
            SkillTriggerHelper.UpdateSkillMaxRadiusTrigger(skill);

            // 更新伤害触发器
            SkillTriggerHelper.UpdateDamageTrigger(skill);

            await ETTask.CompletedTask;
        }
    }
}