using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
	public float damage;
	public bool isDualWielded;
	public WeaponType type;
	public Enchantment enchantment;

	public Weapon()
	{

	}

	public Weapon(WeaponData weaponData) : base(weaponData, false)
	{
		WeaponPrefab weaponPrefab = Resources.Load<WeaponPrefab>("Items/Weapons/" + itemPrefabName);
		damage = weaponData.damage;
		model = weaponPrefab.model;
		sprite = weaponPrefab.sprite;
		isDualWielded = weaponPrefab.isDualWelded;
		type = weaponPrefab.weaponType;
		enchantment = new Enchantment(weaponData.enchantment);
	}
}

public enum WeaponType
{
	Heavy,
	Normal,
	Light
}
