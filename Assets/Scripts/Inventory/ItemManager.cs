using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
	public static Color GetRarityColor(ItemRarity itemRarity)
	{
		switch (itemRarity)
		{
			case ItemRarity.Trash:
				return Color.white;
			case ItemRarity.Normal:
				return Color.green;
			case ItemRarity.Rare:
				return Color.blue;
			case ItemRarity.Epic:
				return Color.magenta;
			case ItemRarity.Legendary:
				return Color.yellow;
			case ItemRarity.Dev:
				return Color.cyan;
			default:
				return Color.red;
		}
	}
}
