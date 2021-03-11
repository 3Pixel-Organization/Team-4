using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks
{
	[CreateAssetMenu(menuName = "Attacks/Attack/Standard")]
	public class SOAttack : ScriptableObject
	{
		public float DamageAmount;
		public LayerMask damageMask;
		public string damageTag;


		public virtual void Initiate()
		{
			
		}
	}
}

