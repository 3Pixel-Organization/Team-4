using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
	public string name;
	public int id;
	public int level;
	public int value;
	public ItemRarity rarity;
	public string itemPrefabName;
	public GameObject model;
	public Sprite sprite;
	static int nextID = 0;

	public Item()
	{
		nextID++;
		id = nextID;
	}

	public Item(string name, int level, int value, ItemRarity rarity, string itemPrefabName)
	{
		nextID++;
		id = nextID;
		this.name = name;
		this.level = level;
		this.value = value;
		this.rarity = rarity;
		this.itemPrefabName = itemPrefabName;
	}

	public Item(ItemData itemData, bool loadModel)
	{
		nextID++;
		id = nextID;
		name = itemData.name;
		level = itemData.level;
		value = itemData.value;
		rarity = itemData.rarity;
		itemPrefabName = itemData.itemPrefabName;
		if (loadModel)
		{
			model = Resources.Load<ItemPrefab>("Items/" + itemPrefabName).model;
			sprite = Resources.Load<ItemPrefab>("Items/" + itemPrefabName).sprite;
		}
	}

	public override bool Equals(object obj)
	{
		//Check for null and compare run-time types.
		if ((obj == null) || !this.GetType().Equals(obj.GetType()))
		{
			return false;
		}
		else
		{
			Item compItem = (Item)obj;
			return id == compItem.id;
		}
	}

	public override int GetHashCode()
	{
		return id;
	}
}

public enum ItemRarity
{
	Trash,
	Normal,
	Rare,
	Epic,
	Legendary,
	Dev
}
