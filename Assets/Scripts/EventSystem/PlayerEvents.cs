using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{
	public class PlayerEvents
	{
		public event Action OnPlayerLevelUp;
		public void PlayerLevelUp()
		{
			OnPlayerLevelUp?.Invoke();
		}

		public event Action<int> OnAbilityCooldownStart;
		public void AbilityCooldownStart(int id)
		{
			OnAbilityCooldownStart?.Invoke(id);
		}

		public event Action<int> OnAbilityCooldownEnd;
		public void AbilityCooldownEnd(int id)
		{
			OnAbilityCooldownEnd?.Invoke(id);
		}

		public event Action OnDamageEnemy;
		public void DamageEnemy()
		{
			OnDamageEnemy?.Invoke();
		}
	}
}
