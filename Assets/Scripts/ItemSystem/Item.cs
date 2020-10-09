using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
	public string name;
	public int level;
	public int value;
	public ItemRarity rarity;
	public string itemPrefabName;
	public GameObject model;
	public Sprite sprite;

	public Item()
	{

	}

	public Item(string name, int level, int value, ItemRarity rarity, string itemPrefabName)
	{
		this.name = name;
		this.level = level;
		this.value = value;
		this.rarity = rarity;
		this.itemPrefabName = itemPrefabName;
	}

	public Item(ItemData itemData, bool loadModel)
	{
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
