using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUI : MonoBehaviour
{
	[SerializeField] private GameObject toEnable;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			toEnable.SetActive(true);
		}
	}
}
