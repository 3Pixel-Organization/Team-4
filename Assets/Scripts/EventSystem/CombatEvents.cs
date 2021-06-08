using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HealthV2;

namespace EventSystem
{
	public class CombatEvents
	{
		public delegate void AttackEventDel(GameObject target, GameObject source, Attack attack, AttackResponse attackResponse);
		public event AttackEventDel OnAttack;

		public event Action<GameObject, GameObject, Attack, AttackResponse> OnDeath;
		public void AttackEvent(GameObject target, GameObject source, Attack attack, AttackResponse attackResponse)
		{
			OnAttack?.Invoke(target, source, attack, attackResponse);
		}

		public void DeathEvent(GameObject target, GameObject source, Attack attack, AttackResponse attackResponse)
		{
			OnDeath?.Invoke(target, source, attack, attackResponse);
		}
	}
}
