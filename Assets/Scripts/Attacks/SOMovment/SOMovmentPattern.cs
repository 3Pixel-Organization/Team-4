using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attacks.Movment
{
	[CreateAssetMenu(menuName = "Attacks/Movment/Pattern")]
	public class SOMovmentPattern : ScriptableObject
	{
		public float speedMultiplier = 1;

		[Expandable] public List<SOMovmentBehavior> movmentBehaviors;

		public void Initiate()
		{
			foreach (SOMovmentBehavior item in movmentBehaviors)
			{
				item.Initiate();
			}
		}

		public void Move(Transform transform)
		{
			foreach (SOMovmentBehavior item in movmentBehaviors)
			{
				item.Move(transform, speedMultiplier);
			}
		}
	}
}

