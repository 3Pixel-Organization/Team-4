using UnityEngine;

namespace Health
{
   public class Damage : MonoBehaviour
   {
	  /// <summary>
	  /// Example implementation of Damage Script 
	  /// </summary>
	  [SerializeField]private int damageAmt;
	  private HealthController _healthController;

	  private void OnCollisionEnter(Collision other)
	  {
		 _healthController = other.gameObject.GetComponent<HealthController>();
		 if (_healthController != null)
		 {
			_healthController.Damage(damageAmt);
		 }
   
	  }

		private void OnTriggerEnter(Collider other)
		{
			_healthController = other.gameObject.GetComponent<HealthController>();
			if (_healthController != null)
			{
				_healthController.Damage(damageAmt);
			}
		}
	}
}
