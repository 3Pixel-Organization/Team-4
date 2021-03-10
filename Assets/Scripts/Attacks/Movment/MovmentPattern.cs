using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks.Movment
{
	[System.Serializable]
	public class MovmentPattern
	{
		public enum Pattern
		{
			Forward,
			SideToSide,
			Turn
		}

		[System.Serializable]
		public struct SideToSideProps
		{
			public float range;
		}

		[System.Serializable]
		public struct RotateProps
		{
			public float angle;
		}

		public Pattern pattern;
		public float speed;

		public SideToSideProps sideToSide;
		public RotateProps rotate;

		private Vector3 startPos;

		float sideOffset = 0;
		bool moveLeft = true;

		public void Start(Transform transform)
		{
			startPos = transform.position;
		}

		public void Move(Transform transform)
		{
			if(pattern == Pattern.Forward)
			{
				Forward(transform);
			}

			if(pattern == Pattern.SideToSide)
			{
				SideToSide(transform);
			}

			if(pattern == Pattern.Turn)
			{
				Rotate(transform);
			}
		}

		void Forward(Transform transform)
		{
			transform.position += transform.rotation * Vector3.forward * speed * Time.deltaTime;
		}

		void SideToSide(Transform transform)
		{
			if(moveLeft)
			{
				transform.position += transform.rotation * Vector3.left * speed * Time.deltaTime;
				sideOffset += speed * Time.deltaTime;
			}
			else
			{
				transform.position += transform.rotation * Vector3.right * speed * Time.deltaTime;
				sideOffset -= speed * Time.deltaTime;
			}
			if(sideOffset > sideToSide.range && moveLeft)
			{
				moveLeft = false;
			}
			else if (sideOffset < -sideToSide.range && !moveLeft)
			{
				moveLeft = true;
			}	
		}

		void Rotate(Transform transform)
		{
			
			transform.rotation *= Quaternion.AngleAxis(rotate.angle * Time.deltaTime, Vector3.up);
		}
	}
}

