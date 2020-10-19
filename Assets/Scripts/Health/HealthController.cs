using UnityEngine;

namespace Health
{
    /// <summary>
    /// Used to manage entity health.
    /// </summary>
    
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private int _currentHealth = 100;
        private float _currentDamageCooldown = 0;

        [Tooltip("Max health this character can have.")]
        public int MaxHealth = 100;

        //todo: implement this
        [Tooltip("Duration (in milliseconds) during which the character cannot take damage.")]
        public int DamageCooldown = 100;

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
        /// Called when entity damage cooldown has ended. 
        /// </summary>
        public event Delegates.StateEvent DamageCooldownEnd;

        /// <summary>
        /// Called when this entity has been killed.
        /// </summary>
        public event Delegates.StateEvent Death;

        /// <summary>
        /// Determines whether this entity is alive or not.
        /// </summary>
        public bool IsAlive => CurrentHealth > 0;

        /// <summary>
        /// Returns the current number of health this character has.
        /// </summary>
        public int CurrentHealth
        {
            get => _currentHealth;
            private set
            {
                // check if health value has changed
                if (_currentHealth == value)
                    return;

                var healthDelta = value - _currentHealth;
                _currentHealth = value;
                OnHealthChange(_currentHealth, MaxHealth, healthDelta);
            }
        }

        /// <summary>
        /// Returns whether this character is invulnerable to damage or not.
        /// </summary>
        public bool IsInvulnerable => _currentDamageCooldown > 0;

        private void Start()
        {
            CurrentHealth = MaxHealth;
        }

        private void Update()
        {
            // update current damage cooldown
            if (IsInvulnerable && DamageCooldown > 0)
            {
                _currentDamageCooldown -= Time.timeScale;

                // check if character is no longer invulnerable
                if (!IsInvulnerable)
                    OnDamageCooldownEnd();


            }
        }

        /// <summary>
        /// Resets the damage cooldown timer, making this character temporarily invulnerable to damage.
        /// </summary>
        private void ResetDamageCooldownTimer()
        {
            _currentDamageCooldown = DamageCooldown;
        }

        /// <summary>
        /// Gives additional health to this character.
        /// </summary>
        /// <param name="amount">Amount of health to give.</param>
        public void GiveHealth(int amount)
        {
            // do not give health if there's nothing to give or if entity is dead
            if (amount == 0 || !IsAlive)
                return;

            var newHealth = CurrentHealth + amount;

            if (newHealth > MaxHealth)
                newHealth = MaxHealth;

            CurrentHealth = newHealth;
            OnHealthIncrease(CurrentHealth, MaxHealth, amount);
        }

        /// <summary>
        /// Damages this entity
        /// </summary>
        /// <param name="amount">Amount of damage to take.</param>
        public void Damage(int amount)
        {
            // do not take damage if character is invulnerable or dead
            if (IsInvulnerable || !IsAlive)
                return;

            var newHealth = CurrentHealth - amount;

            // check if entity survived damage
            if (newHealth <= 0)
            {
                CurrentHealth = 0;
                OnDeath();
            }
            else
            {
                CurrentHealth = newHealth;
                OnTakeDamage(CurrentHealth, MaxHealth, amount);
                ResetDamageCooldownTimer();
            }

           // Debug.Log(CurrentHealth);
        }

        protected virtual void OnHealthChange(int currentHealth, int maxHealth, int healthDelta)
        {
            HealthChange?.Invoke(currentHealth, maxHealth, healthDelta);
        }

        protected virtual void OnHealthIncrease(int currentHealth, int maxHealth, int healthDelta)
        {
            HealthIncrease?.Invoke(currentHealth, maxHealth, healthDelta);
        }

        protected virtual void OnTakeDamage(int currentHealth, int maxHealth, int healthDelta)
        {
            TakeDamage?.Invoke(currentHealth, maxHealth, healthDelta);
        }

        protected virtual void OnDamageCooldownEnd()
        {
            DamageCooldownEnd?.Invoke();
        }

        protected virtual void OnDeath()
        {
            Death?.Invoke();
        }
    }
}