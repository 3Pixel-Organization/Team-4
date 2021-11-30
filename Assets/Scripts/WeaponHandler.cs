using Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
	public Weapon weapon;

	private Damage damage;
	private DepAttack attack;

	public void SetupWeapon(Weapon weapon)
	{
		this.weapon = weapon;
		GameObject weaponObj = Instantiate(weapon.model, transform);
		//attack = weaponObj.GetComponentInChildren<DepAttack>();
		//damage = weaponObj.GetComponentInChildren<Damage>();
		//damage.enabled = false;
	}

	public void StartAttack()
	{
		if(damage != null)
		{
			damage.enabled = true;
		}
		if(attack != null)
		{
			attack.IsAttacking = true;
		}
	}

	public void EndAttack()
	{
		if (damage != null)
		{
			damage.enabled = false;
		}
		if (attack != null)
		{
			attack.IsAttacking = false;
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
