using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnchantmentData
{
	public EnchantmentType type;

	public EnchantmentData(Enchantment enchantment)
	{
		type = enchantment.type;
	}
}
