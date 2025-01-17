﻿namespace ET
{
    public enum NumericType
    {
		Max = 10000,

		// 移动速度
		Speed = 1000,
		SpeedBase = Speed * 10 + 1,
	    SpeedAdd = Speed * 10 + 2,
	    SpeedPct = Speed * 10 + 3,

		// 血量
	    Hp = 1001,
	    HpBase = Hp * 10 + 1,
		HpAdd = Hp * 10 + 2,
		HpPct = Hp * 10 + 3,

		// 最大血量
		MaxHp = 1002,
	    MaxHpBase = MaxHp * 10 + 1,
	    MaxHpAdd = MaxHp * 10 + 2,
	    MaxHpPct = MaxHp * 10 + 3,

		// 攻速
		AttackSpeed = 1003,
		AttackSpeedBase = AttackSpeed * 10 + 1,
		AttackSpeedAdd = AttackSpeed * 10 + 2,
		AttackSpeedPct = AttackSpeed * 10 + 3,

		// 怒气
		Anger = 1004,
		AngerBase = Anger * 10 + 1,

		// 怒气恢复
		AngerRegain = 1005,
		AngerRegainBase = AngerRegain * 10 + 1,
		AngerRegainAdd = AngerRegain * 10 + 2,
		AngerRegainPct = AngerRegain * 10 + 3,

		// 血量恢复
		HPRegain = 1006,
		HPRegainBase = HPRegain * 10 + 1,
		HPRegainAdd = HPRegain * 10 + 2,
		HPRegainPct = HPRegain * 10 + 3,

		// 攻击力
		HPDamage = 1007,
		HPDamageBase = HPDamage * 10 + 1,
		HPDamageAdd = HPDamage * 10 + 2,
		HPDamagePct = HPDamage * 10 + 3,

		// 警戒范围缩放
		AlertRadius = 1008,
		AlertRadiusBase = AlertRadius * 10 + 1,
		AlertRadiusAdd = AlertRadius * 10 + 2,
		AlertRadiusPct = AlertRadius * 10 + 3,


		// 技能数值属性--------------------------
		// 最大攻击半径
		MaxRadius = 1101,
		MaxRadiusBase = MaxRadius * 10 + 1,
		MaxRadiusAdd = MaxRadius * 10 + 2,
		MaxRadiusPct = MaxRadius * 10 + 3,

		// 伤害区域缩放
		ScaleX = 1102,
		ScaleXBase = ScaleX * 10 + 1,
		ScaleXAdd = ScaleX * 10 + 2,
		ScaleXPct = ScaleX * 10 + 3,

		ScaleY = 1102,
		ScaleYBase = ScaleY * 10 + 1,
		ScaleYAdd = ScaleY * 10 + 2,
		ScaleYPct = ScaleY * 10 + 3,

		ScaleZ = 1102,
		ScaleZBase = ScaleZ * 10 + 1,
		ScaleZAdd = ScaleZ * 10 + 2,
		ScaleZPct = ScaleZ * 10 + 3,

		// 基础时常
		SkillTime = 1103,
		SkillTimeBase = SkillTime * 10 + 1,
		SkillTimeAdd = SkillTime * 10 + 2,
		SkillTimePct = SkillTime * 10 + 3,
	}
}
