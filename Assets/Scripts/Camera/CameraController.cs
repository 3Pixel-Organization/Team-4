using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField]
	private CameraSettings settings;

	public GameObject followObj;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = Vector3.Lerp(transform.position, followObj.transform.position + settings.objOffset, settings.followBias * Time.deltaTime);
		//transform.position = followObj.transform.position + settings.objOffset;
		transform.eulerAngles = settings.cameraAngle;
	}
}
