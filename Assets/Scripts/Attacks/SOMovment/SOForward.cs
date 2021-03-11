using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks.Movment
{
	[CreateAssetMenu(menuName = "Attacks/Movment/Forward")]
	public class SOForward : SOMovmentBehavior
	{
		public float Speed;
		public override void Move(Transform transform, float speedMultiplier)
		{
			transform.position += transform.rotation * Vector3.forward * Speed * Time.deltaTime * speedMultiplier;
		}
	}
}

