namespace ET
{
    public enum SkillState
    {
        SkillWait,      // 释放条件没有满足，技能等待
        SkillReady,     // 释放条件满足，技能就绪，等待玩家、AI调用
        SkillExecute,   // 技能执行
        SkillEnd,       // 执行完后，一次性技能，进入结束状态，重复释放技能，进入等待状态
    }
}