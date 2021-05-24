using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HealthV2;

public class Player : HealthSystem
{
	public static Player Instance { get; private set; }

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else
		{
			Debug.LogError("Only one player allowed", gameObject);
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		InstanceHealthSystem(5);
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void Warn(AttackWarning attackWarning)
	{

	}

	protected override void Death()
	{
		base.Death();
		Debug.Log("Player died");
	}
}
