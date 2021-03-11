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

		[Expandable] public SOMovmentPattern movmentPattern;
		[Expandable] public List<SOHitBehavior> hitBehaviors;
		public override void Initiate()
		{
			movmentPattern.Initiate();
			foreach (SOHitBehavior item in hitBehaviors)
			{
				item.Initiate();
			}
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

