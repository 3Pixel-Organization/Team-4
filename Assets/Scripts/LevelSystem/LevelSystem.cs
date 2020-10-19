using System;
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
			_currentExp = Player.expPoints;
			_currentLvl = Player.level;
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
			Player.expPoints = currentExp;
			OnExpChange?.Invoke();
		}

		void LevelChange()
		{
			UpdateExpToNextLevel();
			Player.level = currentLvl;
			OnLevelChange?.Invoke();
		}

		void UpdateExpToNextLevel()
		{
			expToNextLvl = _currentLvl * 1.2f * 100f;
		}

		public void GiveExp(float amount)
		{
			currentExp += amount;
		}
	}
}
