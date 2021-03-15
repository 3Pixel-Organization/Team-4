using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attacks.Movment;
using Attacks.HitBehaviors;

namespace Attacks
{
	[CreateAssetMenu(menuName = "Attacks/Attack/Projectile")]
	public class SOProjectile : SOAttack
	{
		public CollisionType collisionType;
		[Expandable] public SOMovmentPattern movmentPattern;
		[Expandable] public List<SOHitBehavior> hitBehaviors;

		private CollisionType oldCollisionType;
		private SOMovmentPattern oldMovmentPattern;
		public override void Initiate()
		{
			movmentPattern.Initiate();
			foreach (SOHitBehavior item in hitBehaviors)
			{
				item.Initiate();
			}
		}

		public void CollisionUpdate()
		{

		}

		private void OnValidate()
		{
			
		}

		public void Hit(HitData hitData)
		{
			foreach (SOHitBehavior item in hitBehaviors)
			{
				item.Hit(hitData);
			}
		}
	}
}

