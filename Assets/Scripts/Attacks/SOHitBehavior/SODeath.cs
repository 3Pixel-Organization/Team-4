using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks.HitBehaviors
{
	[CreateAssetMenu(menuName = "Attacks/Hit/Death")]
	public class SODeath : SOHitBehavior
	{
		public override void Hit(HitData hitData)
		{
			Destroy(hitData.self);
		}
	}
}

