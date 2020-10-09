using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject LootPrefab;

	// Start is called before the first frame update
	void Start()
	{
		List<Item> itemsToSpawn = new List<Item>
		{
			ItemManager.CreateWeapon("Killer bill", 15, 100, ItemRarity.Rare, "Katana", 100, new Enchantment(EnchantmentType.Fire)),
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

	public void SpawnLoot(Vector3 position, Item item)
	{
		GameObject gameLoot = Instantiate(LootPrefab, position, Quaternion.identity);
		gameLoot.GetComponent<LootDrop>().SetupLoot(item);
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
