using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameEvents : MonoBehaviour
{
	public static GameEvents current;
	private void Awake()
	{
		current = this;
	}

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

	public event Action<int> onEnemySpawnTriggerEnter;
	public void EnemySpawnTriggerEnter(int id)
	{
		if(onEnemySpawnTriggerEnter != null)
		{
			onEnemySpawnTriggerEnter(id);
		}
	}

	public event Action OnPlayerLevelUp;
	public void PlayerLevelUp()
	{
		if (OnPlayerLevelUp != null)
		{
			OnPlayerLevelUp();
		}
	}

	public event Action<int> onAbilityCooldownStart;
	public void AbilityCooldownStart(int id)
	{
		onAbilityCooldownStart?.Invoke(id);
	}

	public event Action<int> onAbilityCooldownEnd;
	public void AbilityCooldownEnd(int id)
	{
		onAbilityCooldownEnd?.Invoke(id);
	}
}
