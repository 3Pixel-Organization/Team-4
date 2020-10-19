using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory
{
	public static List<Item> items = new List<Item>();

	public static bool AddItemToInventory(Item item)
	{
		items.Add(item);
		/*
		if(item is Weapon)
		{
			Weapon weapon = item as Weapon;
			SaveData.Current.items.Add(new WeaponData(weapon));
		}
		else if(item is Armor)
		{
			Armor armor = item as Armor;
			SaveData.Current.items.Add(new ArmorData(armor));
		}
		else
		{
			SaveData.Current.items.Add(new ItemData(item));
		}*/
		return true;
	}

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
