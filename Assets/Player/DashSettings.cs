using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Settings/Dash settings")]
public class DashSettings : ScriptableObject
{
	public float duration;
	public float distance;
	public float speed;
	public float cooldown;
}
