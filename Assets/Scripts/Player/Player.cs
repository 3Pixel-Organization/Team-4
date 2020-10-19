using Levels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
	public static int level;
	public static float expPoints;
	public static Weapon equippedWeapon;
	public static List<Armor> equippedArmor;
	public static LevelSystem levelSystem;

	public static void Save()
	{
		if(SaveData.Current.player == null)
		{
			SaveData.Current.player = new PlayerData();
		}
		if(equippedWeapon != null)
		{
			SaveData.Current.player.currentWeapon = new WeaponData(equippedWeapon);
		}
		if(equippedArmor != null)
		{
			SaveData.Current.player.currentArmor.Clear();

			foreach (Armor item in equippedArmor)
			{
				SaveData.Current.player.currentArmor.Add(new ArmorData(item));
			}
		}
		

		SaveData.Current.player.level = level;
		SaveData.Current.player.expPoints = expPoints;

		SerializationManager.Save("playerData", SaveData.Current.player);
	}

	public static void Load()
	{
		SaveData.Current.player = (PlayerData)SerializationManager.Load(Application.persistentDataPath + "/saves/playerData.savedata");

		if (SaveData.Current.player != null)
		{
			level = SaveData.Current.player.level;
			expPoints = SaveData.Current.player.expPoints;
			levelSystem = new LevelSystem();
			if (SaveData.Current.player.currentWeapon != null)
			{
				equippedWeapon = new Weapon(SaveData.Current.player.currentWeapon);
			}
			if(SaveData.Current.player.currentArmor != null)
			{
				equippedArmor.Clear();
				foreach (ArmorData item in SaveData.Current.player.currentArmor)
				{
					equippedArmor.Add(new Armor(item));
				}
			}
		}
		else
		{
			SaveData.Current.player = new PlayerData()
			{
				level = 0,
				expPoints = 0,
			};
			level = 0;
			expPoints = 0;
			levelSystem = new LevelSystem();
		}
	}
}