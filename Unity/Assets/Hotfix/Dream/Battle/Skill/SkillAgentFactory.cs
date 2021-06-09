namespace ET
{
    public static class SkillAgentFactory
    {
        public static SkillAgent Create(Skill skill)
        {
            SkillAgent skillAgent = new SkillAgent();
            bool bOK = skillAgent.btload(skill.SkillConfig.SkillTree);

            if (bOK)
            {
                skillAgent.btsetcurrent(skill.SkillConfig.SkillTree);

                skillAgent.skill = skill;

                return skillAgent;
            }
            return null;
        }
    }

}