using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - 150 * Time.deltaTime, transform.localEulerAngles.z);
	}
}
