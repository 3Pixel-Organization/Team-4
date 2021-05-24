using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HealthV2
{
	public class AttackWarning
	{
		public GameObject Self { get; private set; }
		public WarningType Type { get; private set; }

		public AttackWarning(GameObject self, WarningType type)
		{
			Self = self;
			Type = type;
		}

		public enum WarningType
		{
			none,
			StartAttack,
			StartBlocking,
			StartCountering
		}
	}
}