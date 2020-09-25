using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObj : MonoBehaviour
{
	public float spinSpeed;
	// Start is called before the first frame update
	void Start()
	{
		transform.eulerAngles = new Vector3(5, 0, 30);
	}

	// Update is called once per frame
	void Update()
	{
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + spinSpeed * Time.deltaTime, transform.eulerAngles.z);
	}
}
