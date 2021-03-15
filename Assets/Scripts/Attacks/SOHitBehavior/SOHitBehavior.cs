using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks.HitBehaviors
{
	public class SOHitBehavior : ScriptableObject
	{
		public virtual void Initiate()
		{

		}
		public virtual void Hit(HitData hitData)
		{

		}
	}

	public class HitData
	{
		public float damageAmount;
		public GameObject self;
	}
}


