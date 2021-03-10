using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HealthV2;
using Attacks;

namespace Attacks.Projectiles
{
	public class Shuriken : Projectile, IDamageable
	{
		public void Damage(float damage)
		{
			transform.rotation *= Quaternion.AngleAxis(180, Vector3.up);
		}
	}
}

