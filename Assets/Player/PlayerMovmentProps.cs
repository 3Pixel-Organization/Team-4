using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Player movment properties")]
public class PlayerMovmentProps : ScriptableObject
{
	public float movementSpeed;
	public float maxSpeed;
	public float moveDampening;
	public float jumpForce;
}
