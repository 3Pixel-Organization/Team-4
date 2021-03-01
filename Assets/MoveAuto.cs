using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAuto : MonoBehaviour
{
	Vector3 startPos;
	// Start is called before the first frame update
	void Start()
	{
		startPos = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = new Vector3(-Mathf.Repeat(-transform.position.x + 5 * Time.deltaTime, 10), transform.position.y, Mathf.Repeat(transform.position.z + 5 * Time.deltaTime, 10));
	}
}
