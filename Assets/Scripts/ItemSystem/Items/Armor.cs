using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item
{
	public float defence;
	public ArmorType type;
	public Enchantment enchantment;

	public Armor()
	{

	}

	public Armor(ArmorData armorData) : base(armorData, false)
	{
		ArmorPrefab armorPrefab = Resources.Load<ArmorPrefab>("Items/Armor/" + itemPrefabName);
		defence = armorData.defence;
		type = armorPrefab.type;
		model = armorPrefab.model;
		sprite = armorPrefab.sprite;
		enchantment = new Enchantment(armorData.enchantment);
	}
}

public enum ArmorType
{
	Head,
	Body,
	Legs,
	Feet
}
