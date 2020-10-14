using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot/LootTable")]
public class LootTable : ScriptableObject
{
	public List<LootPrefab> lootTierEasy;
	public List<LootPrefab> lootTierNormal;
	public List<LootPrefab> lootTierHard;
	public List<LootPrefab> lootTierHarder;
	public List<LootPrefab> lootTierBoss;


	public List<LootPrefab> GetLoot(EnemyTier enemyTier)
	{
		List<LootPrefab> lootList = new List<LootPrefab>();
		List<LootPrefab> returnLoot = new List<LootPrefab>();
		switch (enemyTier)
		{
			case EnemyTier.Easy:
				lootList = lootTierEasy;
				break;
			case EnemyTier.Normal:
				lootList = lootTierNormal;
				break;
			case EnemyTier.Hard:
				lootList = lootTierHard;
				break;
			case EnemyTier.Harder:
				lootList = lootTierHarder;
				break;
			case EnemyTier.Boss:
				lootList = lootTierBoss;
				break;
			default:
				break;
		}
		foreach (LootPrefab item in lootList)
		{
			float generatedChance = Random.Range(0f, 1f);
			if((item.dropChance/100) > generatedChance)
			{
				returnLoot.Add(item);
			}
		}
		return returnLoot;
	}
}
