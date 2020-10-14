using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Loot/LootPrefab")]
public class LootPrefab : ScriptableObject
{
	public string itemName;
	[Range(100.0f, 0.0f)]
	public float dropChance; // 1: 100%, 0: 0%
	public int dropAmount = 1;
	public int levelBoost = 0;
	public float valueMultiplier = 1;
	public ItemPrefab itemPrefab;
	public ItemRarity itemRarity;

	public Item CreateItem(int level)
	{
		Item returnItem;
		string _name = itemName;
		int _level = level + levelBoost;
		int _value = Mathf.RoundToInt(level * valueMultiplier);
		ItemRarity _itemRarity = itemRarity;
		string _itemPrefabName = itemPrefab.name;
		GameObject _model = itemPrefab.model;
		Sprite _sprite = itemPrefab.sprite;

		if(itemPrefab is WeaponPrefab)
		{
			returnItem = new Weapon()
			{
				name = _name,
				level = _level,
				value = _value,
				rarity = _itemRarity,
				itemPrefabName = _itemPrefabName,
				model = _model,
				sprite = _sprite,
				damage = level * valueMultiplier,
				enchantment = new Enchantment(EnchantmentType.None),
				type = (itemPrefab as WeaponPrefab).weaponType,
				isDualWielded = (itemPrefab as WeaponPrefab).isDualWelded
			};
		}
		else if(itemPrefab is ArmorPrefab)
		{
			returnItem = new Armor()
			{
				name = _name,
				level = _level,
				value = _value,
				rarity = _itemRarity,
				itemPrefabName = _itemPrefabName,
				model = _model,
				sprite = _sprite,
				defence = level * valueMultiplier,
				enchantment = new Enchantment(EnchantmentType.None),
				type = (itemPrefab as ArmorPrefab).type
			};
		}
		else
		{
			returnItem = new Item()
			{
				name = _name,
				level = _level,
				value = _value,
				rarity = _itemRarity,
				itemPrefabName = _itemPrefabName,
				model = _model,
				sprite = _sprite
			};
		}
		return returnItem;
	}
}