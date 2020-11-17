using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
	[SerializeField] private int areaID;

	private bool triggerd = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !triggerd)
		{
			GameEvents.current.EnemySpawnTriggerEnter(areaID);
			triggerd = true;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position, transform.rotation * new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z));
	}
}
