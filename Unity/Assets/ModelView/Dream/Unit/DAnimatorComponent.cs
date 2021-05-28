using System.Collections.Generic;
using UnityEngine;

namespace ET
{
	public enum DMotionType
	{
		None,
		Idle,
		Run,
	}

	public class DAnimatorComponent : Entity
	{
		public Dictionary<string, AnimationClip> animationClips = new Dictionary<string, AnimationClip>();
		public HashSet<string> Parameter = new HashSet<string>();

		public DMotionType MotionType;
		public float MontionSpeed;
		public bool isStop;
		public float stopSpeed;
		public Animator Animator;
	}
}