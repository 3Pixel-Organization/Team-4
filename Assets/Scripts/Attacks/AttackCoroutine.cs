using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCoroutine : MonoBehaviour
{
	public static IEnumerator AttackEnum()
	{
		yield return new WaitForSeconds(0.5f);
	}

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
