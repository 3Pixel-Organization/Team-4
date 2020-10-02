using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Health
{
    [DisallowMultipleComponent]
    public class HealthBar : MonoBehaviour
    {
        private Slider _barSlider;

        [FormerlySerializedAs("playerHealthController")] public HealthController healthController;
    
        private void Start()
        {
            healthController.HealthChange += OnHealthControllerChanged;
            OnHealthControllerChanged(healthController.CurrentHealth, healthController.MaxHealth, 0);

            _barSlider = GetComponent<Slider>();
           _barSlider.maxValue = healthController.MaxHealth;

        }

        public void OnHealthControllerChanged(int currentHealth, int maxHealth, int healthDelta)
        {
            SetFillPercentage(currentHealth);
            
        }

        private void OnDestroy()
        {
            healthController.HealthChange -= OnHealthControllerChanged;
        }

        public void SetFillPercentage(int value)
        {
            if (_barSlider != null)
            {
               // Debug.Log(value);
                _barSlider.value = value;
            }
        }
    }
}
