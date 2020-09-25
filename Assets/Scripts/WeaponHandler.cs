using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
	public Weapon weapon;

	public void SetupWeapon(Weapon weapon)
	{
		this.weapon = weapon;
		GameObject weaponObj = Instantiate(weapon.model, transform);
		
	}
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
