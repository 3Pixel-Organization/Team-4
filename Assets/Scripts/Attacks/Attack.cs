using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HealthV2;

namespace Attacks
{
	public class Attack : MonoBehaviour, IDamageable
	{
		[System.Serializable]
		public struct AttackProps
		{
			public float DamageAmount;
		}

		//[SerializeField] public AttackProps attackProps;

		public virtual void Damage(float damage)
		{
			
		}
	}
}

