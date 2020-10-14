using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
	public static GameManager current;
	[SerializeField] private GameObject LootPrefab;
	[SerializeField] private GameObject inventoryCanvas;
	[SerializeField] private GameObject gameplayCanvas;

	public List<Item> currentRunItems = new List<Item>();
	public LevelData levelData;

	private void Awake()
	{
		current = this;
		Inventory.Load();
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
			SpawnLoot(new Vector3(-13, 1, -3), item);
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
			Inventory.AddItemToInventory(item);
		}
		currentRunItems.Clear();
		Inventory.Save();
	}

	public void Loose()
	{

	}

	void PauseGame()
	{
		Time.timeScale = 0;
	}

	void UnPauseGame()
	{
		Time.timeScale = 1;
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

	public void OpenInventory()
	{
		PauseGame();
		inventoryCanvas.SetActive(true);
		gameplayCanvas.SetActive(false);
	}

	public void CloseInventory()
	{
		UnPauseGame();
		inventoryCanvas.SetActive(false);
		gameplayCanvas.SetActive(true);
	}

	public void Load()
	{
		Inventory.Load();
	}

	public void Save()
	{
		Inventory.Save();
	}

	public void PrintInventory()
	{
		foreach (ItemData item in SaveData.Current.items)
		{
			Debug.Log("name: " + item.name + ", rarity: " + item.rarity);
		}
	}
}
