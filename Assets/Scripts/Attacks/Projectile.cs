using UnityEngine;
using Attacks.Movment;
using System.Collections.Generic;

namespace Attacks
{
	public class Projectile : Attack
	{
		[System.Serializable]
		public struct MovmentProps
		{
			public List<MovmentPattern> movmentPatterns;
		}


		[SerializeField] public MovmentProps movment;

		private void Update()
		{
			foreach (MovmentPattern item in movment.movmentPatterns)
			{
				item.Move(transform);
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			Destroy(gameObject, 0.1f);
		}
	}
}
