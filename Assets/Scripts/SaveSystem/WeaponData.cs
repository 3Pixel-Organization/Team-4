using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData : ItemData
{
	public float damage;
	public EnchantmentData enchantment;

	public WeaponData(Weapon weapon) : base(weapon)
	{
		damage = weapon.damage;
		enchantment = new EnchantmentData(weapon.enchantment);
	}
}
