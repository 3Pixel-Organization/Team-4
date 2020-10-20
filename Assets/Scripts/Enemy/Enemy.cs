using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Health;

public class Enemy : MonoBehaviour
{
	public int level;
	public TMPro.TextMeshProUGUI levelText;
	public EnemyPrefab enemyPrefab;

	private HealthController healthController;
	// Start is called before the first frame update
	void Start()
	{
		healthController = GetComponent<HealthController>();
		healthController.Death += Death;
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void Death()
	{
		GameManager.current.EnemyDeath(transform, this);
		Player.levelSystem.GiveExp((GameManager.current.levelData.difficultyLevel + enemyPrefab.reletiveLevel) * enemyPrefab.expMultiplier * 10);
		if(enemyPrefab.enemyTier == EnemyTier.Boss)
		{
			GameManager.current.Victory();
		}
		Destroy(this.gameObject);
	}
}
