using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks
{
	public class Attack : MonoBehaviour
	{
		[System.Serializable]
		public struct AttackProps
		{
			public float DamageAmount;
		}

		[SerializeField] public AttackProps attackProps;
	}
}

