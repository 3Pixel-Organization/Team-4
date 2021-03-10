﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
	public class LevelSystem 
	{
		private float _currentExp;
		float currentExp
		{
			get => _currentExp;
			set
			{
				if(_currentExp == value)
				{
					return;
				}
				_currentExp = value;
				ExpChange();
			}
		}
		private int _currentLvl;
		int currentLvl
		{
			get => _currentLvl;
			set
			{
				if(_currentLvl == value)
				{
					return;
				}
				_currentLvl = value;
				LevelChange();
			}
		}

		float expToNextLvl;
		int maxLevel = 20;
		public LevelSystem()
		{
			_currentExp = PlayerData.expPoints;
			_currentLvl = PlayerData.level;
			UpdateExpToNextLevel();
		}

		public event Action OnExpChange;
		public event Action OnLevelChange;

		void ExpChange()
		{
			if(currentLvl == maxLevel && currentExp > 0)
			{
				currentExp = 0;
			}
			while(currentExp >= expToNextLvl)
			{
				if (currentLvl == maxLevel && currentExp > 0)
				{
					_currentExp = 0;
					break;
				}
				_currentExp -= expToNextLvl;
				currentLvl++;
			}
			PlayerData.expPoints = currentExp;
			OnExpChange?.Invoke();
		}

		void LevelChange()
		{
			UpdateExpToNextLevel();
			PlayerData.level = currentLvl;
			OnLevelChange?.Invoke();
		}

		void UpdateExpToNextLevel()
		{
			expToNextLvl = Exp.ExpForLvl(_currentLvl);
		}

		public void GiveExp(float amount)
		{
			currentExp += amount;
		}

		/// <summary>
		/// Do not use this is only ment to be used by the debug console
		/// </summary>
		/// <param name="amount">the xp amount</param>
		public void SetExp(float amount)
		{

		}

		/// <summary>
		/// Do not use this is only ment to be used by the debug console
		/// </summary>
		/// <param name="level">the level</param>
		public void SetLvl(int level)
		{

		}
	}
}
