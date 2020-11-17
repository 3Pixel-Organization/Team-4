using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Inventory
{
	public static List<Item> items = new List<Item>();

	/// <summary>
	/// Adds an item to the inventory
	/// </summary>
	/// <param name="item">item to be added</param>
	/// <returns>success or fail</returns>
	public static bool AddItem(Item item)
	{
		if (!items.Contains(item))
		{
			items.Add(item);
			return true;
		}
		else
		{
			return false;
		}
	}

	/// <summary>
	/// Checks if the item is in the inventory
	/// </summary>
	/// <param name="item">item to check for</param>
	/// <returns>if the item is in the inventory</returns>
	public static bool CheckForItem(Item item)
	{
		return items.Contains(item);
	}

	/// <summary>
	/// Removes an item from the inventory
	/// </summary>
	/// <param name="item">item to be removed</param>
	/// <returns>success or fail</returns>
	public static bool RemoveItem(Item item)
	{
		return items.Remove(item);
	}

	/// <summary>
	/// In editor help to clear player inventory
	/// </summary>
	[MenuItem("Tools/Inventory/Clear inventory save")]
	private static void ClearInventorySave()
	{
		items.Clear();
		Save();
	}

	/// <summary>
	/// Loads the inventory from a file
	/// </summary>
	public static void Load()
	{
		SaveData.Current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/data.savedata");

		items.Clear();
		foreach (ItemData item in SaveData.Current.items)
		{
			if (item is WeaponData)
			{
				WeaponData weapon = item as WeaponData;
				items.Add(new Weapon(weapon));
			}
			else if (item is ArmorData)
			{
				ArmorData armor = item as ArmorData;
				items.Add(new Armor(armor));
			}
			else
			{
				items.Add(new Item(item, true));
			}
		}
	}

	/// <summary>
	/// Saves the current inventory to a file
	/// </summary>
	public static void Save()
	{
		SaveData.Current.items.Clear();
		
		foreach (Item item in items)
		{
			if (item is Weapon)
			{
				Weapon weapon = item as Weapon;
				SaveData.Current.items.Add(new WeaponData(weapon));
			}
			else if (item is Armor)
			{
				Armor armor = item as Armor;
				SaveData.Current.items.Add(new ArmorData(armor));
			}
			else
			{
				SaveData.Current.items.Add(new ItemData(item));
			}
		}
		SerializationManager.Save("data", SaveData.Current);
	}
}
