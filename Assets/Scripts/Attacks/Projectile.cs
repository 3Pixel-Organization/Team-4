using UnityEngine;
using Attacks.Movment;
using System.Collections.Generic;
using Attacks.HitBehaviors;
using HealthV2;

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
			if (!collision.gameObject.CompareTag(projectileData.damageTag)) return;

			HitData hitData = new HitData()
			{
				damageAmount = 1,
				self = gameObject
			};
			projectileData.Hit(hitData);
			if(collision.gameObject.TryGetComponent(typeof(IDamageable), out Component component))
			{
				IDamageable damageable = component as IDamageable;
				damageable.Damage(new HealthV2.Attack(projectileData.DamageAmount));
			}
		}

		public override AttackResponse Damage(HealthV2.Attack attack)
		{
			if (projectileData == null) return new AttackResponse(0,AttackResponse.HitResult.None);

			HitData hitData = new HitData()
			{
				damageAmount = attack.Damage,
				attack = attack,
				self = gameObject
			};
			projectileData.Hit(hitData);
			return new AttackResponse(attack);
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
