using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Levels;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager current;
	[SerializeField] private GameObject LootPrefab;

	public List<Item> currentRunItems = new List<Item>();
	public SceneData levelData;

	[SerializeField] private PlayableDirector victoryTimeline;

	private void Awake()
	{
		current = this;
		Inventory.Load();
		Player.Load();
	}

	// Start is called before the first frame update
	void Start()
	{
		List<Item> itemsToSpawn = new List<Item>
		{
			ItemManager.CreateWeapon("Killer bill", 15, 100, ItemRarity.Legendary, "Katana", 100, new Enchantment(EnchantmentType.Fire)),
		};
		foreach (Item item in itemsToSpawn)
		{
			//SpawnLoot(new Vector3(-13, 1, -3), item);
		}
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void StartLevel()
	{

	}

	public void Victory()
	{
		Debug.Log("Victory");
		foreach (Item item in currentRunItems)
		{
			Inventory.AddItem(item);
		}
		currentRunItems.Clear();
		Inventory.Save();
		Player.Save();
		victoryTimeline.Play();
		//SceneManager.LoadScene("Level00");
	}

	public void Loose()
	{

	}

	public void PauseGame()
	{
		Time.timeScale = 0;
	}

	public void UnPauseGame()
	{
		Time.timeScale = 1;
	}

	public void EnemyDeath(Transform transform, Enemy enemy)
	{
		Player.levelSystem.GiveExp(Exp.EnemyExp(levelData.difficultyLevel + enemy.enemyPrefab.reletiveLevel, enemy.enemyPrefab.expMultiplier));
		List<LootPrefab> prefabLoot = levelData.levelLootTable.GetLoot(enemy.enemyPrefab.enemyTier);
		foreach (LootPrefab item in prefabLoot)
		{
			GameObject gameLoot = Instantiate(LootPrefab, transform.position, Quaternion.identity);
			gameLoot.GetComponent<LootDrop>().SetupLoot(item.CreateItem(levelData.difficultyLevel));
		}
		if(enemy.enemyPrefab.enemyTier == EnemyTier.Boss)
		{
			Victory();
		}
	}

	public void SpawnLoot(Vector3 position, Enemy enemy)
	{
		List<LootPrefab> prefabLoot = levelData.levelLootTable.GetLoot(enemy.enemyPrefab.enemyTier);
		foreach (LootPrefab item in prefabLoot)
		{
			GameObject gameLoot = Instantiate(LootPrefab, position, Quaternion.identity);
			gameLoot.GetComponent<LootDrop>().SetupLoot(item.CreateItem(levelData.difficultyLevel));
		}
	}

	public void SpawnLoot(Vector3 position, Item item)
	{
		GameObject gameLoot = Instantiate(LootPrefab, position, Quaternion.identity);
		gameLoot.GetComponent<LootDrop>().SetupLoot(item);
	}

	public void PickupItem(Item item)
	{
		currentRunItems.Add(item);
	}

	public void PrintInventory()
	{
		foreach (ItemData item in SaveData.Current.items)
		{
			Debug.Log("name: " + item.name + ", rarity: " + item.rarity);
		}
	}
}
