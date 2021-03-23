using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCone : MonoBehaviour
{
	public static IEnumerator AttackConeCorutine(GameObject gameObject)
	{
		yield return new WaitForSeconds(0.5f);
	}
}
