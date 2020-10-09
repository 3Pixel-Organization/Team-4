using Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
	public Weapon weapon;

	private Damage damage;

	public void SetupWeapon(Weapon weapon)
	{
		this.weapon = weapon;
		GameObject weaponObj = Instantiate(weapon.model, transform);
		damage = weaponObj.GetComponentInChildren<Damage>();
		damage.enabled = false;
	}

	public void StartAttack()
	{
		if(damage != null)
		{
			damage.enabled = true;
		}
	}

	public void EndAttack()
	{
		if (damage != null)
		{
			damage.enabled = false;
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
}
