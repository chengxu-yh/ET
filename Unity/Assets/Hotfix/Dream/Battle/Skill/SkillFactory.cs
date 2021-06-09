using UnityEngine;

namespace ET
{
    public static class SkillFactory
    {
        public static Skill Create(Entity parent, int configid)
        {
            Skill skill = EntityFactory.CreateWithParent<Skill, int>(parent, configid);

            return skill;
        }
    }
}
