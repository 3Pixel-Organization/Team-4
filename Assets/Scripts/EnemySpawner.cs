using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private int areaID;
	[SerializeField] private EnemyPrefab enemyPrefab;

	private void Start()
	{
		GameEvents.current.onEnemySpawnTriggerEnter += SpawnEnemy;
	}

	private void SpawnEnemy(int areaID)
	{
		if(this.areaID == areaID)
		{
			int finalLevel = GameManager.current.levelData.difficultyLevel + enemyPrefab.reletiveLevel;
			GameObject enemyObj = Instantiate(enemyPrefab.prefab, transform);
			Enemy enemyScript = enemyObj.GetComponent<Enemy>();
			enemyScript.level = finalLevel;
			enemyScript.levelText.SetText("lvl - " + finalLevel);
		}
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position, Vector3.one);
	}
}
