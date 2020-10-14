using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArmorData : ItemData
{
	public float defence;
	public EnchantmentData enchantment;

	public ArmorData(Armor armor) : base(armor)
	{
		defence = armor.defence;
		enchantment = new EnchantmentData(armor.enchantment);
	}
}
