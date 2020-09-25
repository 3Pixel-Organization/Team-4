using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player settings")]
public class PlayerSettings : ScriptableObject
{
	public float movementSpeed;
	public float maxSpeed;
	public float moveDampening;
	public float jumpForce;
}
