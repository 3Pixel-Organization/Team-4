using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
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
