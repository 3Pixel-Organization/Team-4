using UnityEngine;
using UnityEngine.UI;

namespace Health
{
    [DisallowMultipleComponent]
    public class HealthBar : MonoBehaviour
    {
        private Slider _barSlider;

        public HealthController playerHealthController;
    
        private void Start()
        {
            playerHealthController.HealthChange += OnPlayerPlayerHealthControllerChanged;
            OnPlayerPlayerHealthControllerChanged(playerHealthController.CurrentHealth, playerHealthController.MaxHealth, 0);

            _barSlider = GetComponent<Slider>();
           _barSlider.maxValue = playerHealthController.MaxHealth;

        }

        public void OnPlayerPlayerHealthControllerChanged(int currentHealth, int maxHealth, int healthDelta)
        {
            SetFillPercentage(currentHealth);
            
        }

        private void OnDestroy()
        {
            playerHealthController.HealthChange -= OnPlayerPlayerHealthControllerChanged;
        }

        public void SetFillPercentage(int value)
        {
            if (_barSlider != null)
            {
                Debug.Log(value);
                _barSlider.value = value;
            }
        }
    }
}
