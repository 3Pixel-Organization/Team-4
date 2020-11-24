using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventsSystem
{
	public class SpawnEvents
	{
		public event Action<int> OnEnemySpawnTriggerEnter;
		public void EnemySpawnTriggerEnter(int id)
		{
			OnEnemySpawnTriggerEnter?.Invoke(id);
		}
	}
}
