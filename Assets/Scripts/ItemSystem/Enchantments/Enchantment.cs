using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enchantment
{
	public EnchantmentType type;

	public Enchantment(EnchantmentType type)
	{
		this.type = type;
	}

	public Enchantment(EnchantmentData enchantmentData)
	{
		type = enchantmentData.type;
	}
}

public enum EnchantmentType
{
	None,
	Fire
}