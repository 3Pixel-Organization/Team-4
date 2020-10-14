using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
	public string name;
	public int level;
	public int value;
	public ItemRarity rarity;
	public string itemPrefabName;

	public ItemData(Item item)
	{
		name = item.name;
		level = item.level;
		value = item.value;
		rarity = item.rarity;
		itemPrefabName = item.itemPrefabName;
	}
}
