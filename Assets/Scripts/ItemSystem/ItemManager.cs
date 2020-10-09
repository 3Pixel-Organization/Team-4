using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
	public static Item CreateItem(string name, int level, int value, ItemRarity rarity, string itemPrefabName)
	{
		ItemPrefab itemPrefab = Resources.Load<ItemPrefab>("Items/" + itemPrefabName);
		Item item = new Item
		{
			name = name,
			level = level,
			value = value,
			rarity = rarity,
			model = itemPrefab.model,
			itemPrefabName = itemPrefabName,
		};
		return item;
	}

	public static Weapon CreateWeapon(string name, int level, int value, ItemRarity rarity, string itemPrefabName, float damage, Enchantment enchantment)
	{
		WeaponPrefab weaponPrefab = Resources.Load<WeaponPrefab>("Items/Weapons/" + itemPrefabName);
		Weapon weapon = new Weapon
		{
			name = name,
			level = level,
			value = value,
			rarity = rarity,
			itemPrefabName = itemPrefabName,
			model = weaponPrefab.model,
			damage = damage,
			enchantment = enchantment,
			type = weaponPrefab.weaponType,
			isDualWielded = weaponPrefab.isDualWelded
		};
		return weapon;
	}

	public static Armor CreateArmor(string name, int level, int value, ItemRarity rarity, string itemPrefabName, float defence, Enchantment enchantment)
	{
		ArmorPrefab armorPrefab = Resources.Load<ArmorPrefab>("Items/Armor/" + itemPrefabName);
		Armor armor = new Armor
		{
			name = name,
			level = level,
			value = value,
			rarity = rarity,
			itemPrefabName = itemPrefabName,
			model = armorPrefab.model,
			defence = defence,
			enchantment = enchantment,
			type = armorPrefab.type
		};
		return armor;
	}

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
