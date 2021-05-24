using Health;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using EventSystem;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private int areaID;
	[SerializeField] private EnemyPrefab enemyPrefab;

	private void Start()
	{
		GameEvents.current.spawn.OnEnemySpawnTriggerEnter += SpawnEnemy;
	}

	private void SpawnEnemy(int areaID)
	{
		if(this.areaID == areaID)
		{
			int finalLevel = GameManager.current.levelData.difficultyLevel + enemyPrefab.reletiveLevel;
			GameObject enemyObj = Instantiate(enemyPrefab.prefab, transform.position, transform.rotation);
			//enemyObj.GetComponent<Transform>().position = transform.position;
			//Enemy enemyScript = enemyObj.GetComponent<Enemy>();
			//enemyScript.level = finalLevel;
			//enemyScript.levelText.SetText("lvl - " + finalLevel);
			//enemyScript.enemyPrefab = enemyPrefab;
			//HealthController healthController = enemyObj.GetComponent<HealthController>();
			//healthController.MaxHealth = Mathf.RoundToInt(enemyPrefab.healthMultiplier * (enemyPrefab.reletiveLevel + GameManager.current.levelData.difficultyLevel));
			//healthController.GiveHealth(10000);
		}
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position, Vector3.one);
	}

}
