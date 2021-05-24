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
		public void AttackEvent(GameObject target, GameObject source, Attack attack, AttackResponse attackResponse)
		{
			OnAttack?.Invoke(target, source, attack, attackResponse);
		}
	}
}
