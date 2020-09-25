using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
	private Transform mainCamera;

	// Start is called before the first frame update
	void Start()
	{
		mainCamera = Camera.main.transform;
	}

	// Update is called once per frame
	void Update()
	{
		transform.eulerAngles = mainCamera.eulerAngles;
	}
}
