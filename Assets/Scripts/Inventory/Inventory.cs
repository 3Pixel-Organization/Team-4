using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
	public static List<Item> items = new List<Item>();
	

	public static bool AddItemToInventory(Item item)
	{
		items.Add(item);
		return true;
	}
}
