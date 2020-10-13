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
}
