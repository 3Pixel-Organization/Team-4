using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
class PlayerData
{
	public int level;
	public WeaponData currentWeapon;
	public List<ArmorData> currentArmor;
}
