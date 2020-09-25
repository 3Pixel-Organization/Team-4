using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
	public string name;
	public int level;
	public ItemRarity rarity;

	public Item(string name, int level, ItemRarity rarity)
	{
		this.name = name;
		this.level = level;
		this.rarity = rarity;
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
