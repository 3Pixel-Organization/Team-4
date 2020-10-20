using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
	public class Exp
	{
		public static float ExpForLvl(int level)
		{
			return level * 1.2f * 100f;
		}

		public static float EnemyExp(int level, float multiplier)
		{
			return level * multiplier * 10;
		}
	}
}
