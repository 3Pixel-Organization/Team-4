using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks.Movment
{
	[CreateAssetMenu(menuName = "Attacks/Movment/Side to side")]
	public class SOSideToSide : SOMovmentBehavior
	{
		public float Speed;
		public float range;
		public bool invert = false;

		float sideOffset = 0;
		bool moveLeft = false;

		public override void Initiate()
		{
			moveLeft = invert;
		}

		public override void Move(Transform transform, float speedMultiplier)
		{
			if (moveLeft)
			{
				transform.position += transform.rotation * Vector3.left * Speed * Time.deltaTime * speedMultiplier;
				sideOffset += Speed * Time.deltaTime;
			}
			else
			{
				transform.position += transform.rotation * Vector3.right * Speed * Time.deltaTime * speedMultiplier;
				sideOffset -= Speed * Time.deltaTime;
			}
			if (sideOffset > range && moveLeft)
			{
				moveLeft = false;
			}
			else if (sideOffset < -range && !moveLeft)
			{
				moveLeft = true;
			}
		}
	}
}

