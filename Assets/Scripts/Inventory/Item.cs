using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/item")]
public class Item : ScriptableObject
{
	public string name;
	public int level;
	public int value;
	public ItemRarity rarity;
	public GameObject model;
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
