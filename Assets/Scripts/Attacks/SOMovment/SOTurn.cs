using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks.Movment
{
	[CreateAssetMenu(menuName = "Attacks/Movment/Turn")]
	public class SOTurn : SOMovmentBehavior
	{
		public float rotateByAngle;
		public bool invert;

		public override void Initiate()
		{
			if (invert)
			{
				rotateByAngle = -rotateByAngle;
			}
		}

		public override void Move(Transform transform, float speedMultiplier)
		{
			transform.rotation *= Quaternion.AngleAxis(rotateByAngle * Time.deltaTime, Vector3.up);
		}
	}
}

