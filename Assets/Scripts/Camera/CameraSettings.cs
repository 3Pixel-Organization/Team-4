using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Camera settings")]
public class CameraSettings : ScriptableObject
{
	public Vector3 objOffset;
	public Vector3 cameraAngle;
	public float followBias;
}
