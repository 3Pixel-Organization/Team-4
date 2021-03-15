using UnityEngine;
using Attacks.Movment;
using System.Collections.Generic;
using Attacks.HitBehaviors;

namespace Attacks
{
	public class Projectile : Attack
	{
		private void Start()
		{
			if (projectileData == null) return;
			projectileData.Initiate();
		}


		//[SerializeField] public MovmentProps movment;
		[SerializeField] [Expandable] private SOProjectile projectileData;

		private void Update()
		{
			if (projectileData == null) return;

			projectileData.movmentPattern.Move(transform);
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (projectileData == null) return;

			HitData hitData = new HitData()
			{
				damageAmount = 1,
				self = gameObject
			};
			projectileData.Hit(hitData);
		}

		public override void Damage(float damage)
		{
			if (projectileData == null) return;

			HitData hitData = new HitData()
			{
				damageAmount = damage,
				self = gameObject
			};
			projectileData.Hit(hitData);
		}
	}

	[System.Flags]
	public enum CollisionType
	{
		None = 0,
		Collider = 1,
		Ray = 2,
		Physics = 4
	}
}
