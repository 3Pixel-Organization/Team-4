using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace HealthV2
{
	public class HealthSystem : MonoBehaviour, IDamageable
	{
		/// <summary>
		/// Invoked when entity health has changed.
		/// </summary>
		public event Delegates.HealthEvent HealthChange;

		/// <summary>
		/// Invoked when entity received additional health.
		/// </summary>
		public event Delegates.HealthEvent HealthIncrease;

		/// <summary>
		/// Invoked when entity takes damage.
		/// </summary>
		public event Delegates.HealthEvent TakeDamage;

		/// <summary>
		/// Called when this entity has been killed.
		/// </summary>
		public event Delegates.StateEvent Died;

		/// <summary>
		/// Determines whether this entity is alive or not.
		/// </summary>
		public bool IsAlive => CurrentHealth > 0;

		private float currentHealth;

		/// <summary>
		/// Returns the current number of health this character has.
		/// </summary>
		public float CurrentHealth { get => currentHealth; }

		

		public void InstanceHealthSystem(float currentHealth)
		{
			this.currentHealth = currentHealth;
		}

		public void Damage(float damage)
		{
			currentHealth -= damage;
			if(currentHealth <= 0)
			{
				Death();
			}
		}

		protected virtual void Death()
		{
			Died?.Invoke();
		}

		protected virtual void OnHealthChange(float currentHealth, float maxHealth, float healthDelta)
		{
			HealthChange?.Invoke(currentHealth, maxHealth, healthDelta);
		}

		protected virtual void OnHealthIncrease(float currentHealth, float maxHealth, float healthDelta)
		{
			HealthIncrease?.Invoke(currentHealth, maxHealth, healthDelta);
		}

		protected virtual void OnTakeDamage(float currentHealth, float maxHealth, float healthDelta)
		{
			TakeDamage?.Invoke(currentHealth, maxHealth, healthDelta);
		}
	}
}
