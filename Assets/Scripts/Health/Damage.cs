using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Health
{
   public class Damage : MonoBehaviour
   {
	  /// <summary>
	  /// Example implementation of Damage Script 
	  /// </summary>
	  [SerializeField] public int damageAmt;

	  [SerializeField] private float checkSphereRadius = 1f;
	  [SerializeField] private float checkFrequency = 2f;
	  [SerializeField] private LayerMask CheckLayer;
	  private HealthController _healthController;

	  

	  private void FixedUpdate()
	  {
		 StartCoroutine(GiveDamage());
		
	  }

	  public IEnumerator GiveDamage()
	  {
		 Collider[] colliders = Physics.OverlapSphere(transform.position, checkSphereRadius,CheckLayer);
		 foreach (var index in colliders)
		 {
			if (index.transform != transform)
			{
			   _healthController = index.gameObject.GetComponentInParent<HealthController>();
			   
			   if (_healthController != null)
			   {
				  _healthController.Damage(damageAmt);
				  Debug.Log(gameObject.name + " Does " + damageAmt + " To " + index.name);
			   }
			   
			}
		 }
		 yield return new WaitForSeconds(checkFrequency);
	  }

	  private void OnDrawGizmos()
	  {
		 Gizmos.DrawWireSphere(transform.position,checkSphereRadius);
		 
	  }
   }
}