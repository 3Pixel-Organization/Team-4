using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventsSystem
{
	public class LevelEvents
	{
		public event Action OnLevelStart;
		public void LevelStart()
		{
			OnLevelStart?.Invoke();
		}

		public event Action OnLevelEnd;
		public void LevelEnd()
		{
			OnLevelEnd?.Invoke();
		}
	}
}
