using UnityEngine;

namespace ET
{
    public static class SkillTriggerHelper
    {
        public static GameObject CreateSkillTrigger(Skill skill)
        {
            GameObject trigger = skill.AddComponent<DGameObjectComponent>().Init("Trigger.unity3d", "Trigger", "SkillTrigger", GlobalComponent.Instance.Trigger);
            
            ReferenceCollector rc = trigger.GetComponent<ReferenceCollector>();
            rc.Get<GameObject>("TargetAreaTrigger").SetActive(false);
            rc.Get<GameObject>("CasterAreaTrigger").SetActive(false);
            rc.Get<GameObject>("CasterFrontTrigger").SetActive(false);

            trigger.AddComponent<ComponentView>().Component = skill;

            return trigger;
        }

        public static void UpdateSkillMaxRadiusTrigger(Skill skill)
        {
            DGameObjectComponent objectcom = skill.GetComponent<DGameObjectComponent>();
            NumericComponent skillnum = skill.GetComponent<NumericComponent>();

            float maxradius = skillnum.GetAsFloat(NumericType.MaxRadius);

            ReferenceCollector rc = objectcom.GameObject.GetComponent<ReferenceCollector>();
            GameObject MaxRadiusTrigger = rc.Get<GameObject>("MaxRadiusTrigger");

            MaxRadiusTrigger.transform.localScale = new Vector3(maxradius, maxradius, maxradius);
        }

        public static void UpdateDamageTrigger(Skill skill)
        {
            DGameObjectComponent objectcom = skill.GetComponent<DGameObjectComponent>();
            ReferenceCollector rc = objectcom.GameObject.GetComponent<ReferenceCollector>();

            GameObject trigger = null;
            switch (skill.SkillDamageType)
            {
                case SkillDamageType.TargetArea:
                    trigger = rc.Get<GameObject>("TargetAreaTrigger");
                    break;
                case SkillDamageType.CasterArea:
                    trigger = rc.Get<GameObject>("CasterAreaTrigger");
                    break;
                case SkillDamageType.CasterFront:
                    trigger = rc.Get<GameObject>("CasterFrontTrigger");
                    break;
            }

            if (trigger == null)
            {
                return;
            }

            NumericComponent skillnum = skill.GetComponent<NumericComponent>();

            float scalex = skillnum.GetAsFloat(NumericType.ScaleX);
            float scaley = skillnum.GetAsFloat(NumericType.ScaleY);
            float scalez = skillnum.GetAsFloat(NumericType.ScaleX);

            trigger.SetActive(true);
            trigger.transform.localScale = new Vector3(scalex, scaley, scalez);
        }
    }
}