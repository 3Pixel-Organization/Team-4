using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
	[SerializeField] private Animator animator;
	[SerializeField] private Transform rightHandWeapon = null;
	[SerializeField] private Transform leftHandWeapon = null;
	[SerializeField] private Transform rightWeaponHolder = null;
	[SerializeField] private Transform leftWeaponHolder = null;
	[SerializeField] private Transform backWeaponHolder = null;

	public bool WeaponDrawn
	{
		get => WeaponDrawn;
		set
		{
			if(WeaponDrawn != value)
			{
				if (value)
				{
					DrawWeapon();
				}
				else if (!value)
				{
					ShethWeapon();
				}
				WeaponDrawn = value;
			}
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void DrawWeapon()
	{

	}

	void ShethWeapon()
	{

	}
}
