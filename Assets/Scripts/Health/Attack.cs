using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Health;

public class Attack : MonoBehaviour
{
	private bool isAttacking = false;

	public bool IsAttacking
	{
		get { 
			return isAttacking; 
		}
		set { 
			if(!isAttacking && value)
			{
				StartAttack();
				isAttacking = true;
			}
			if(isAttacking && !value)
			{
				isAttacking = false;
				GiveDamage();
				EndAttack();
			}
		}
	}

	public int damageAmt;

	[SerializeField] private int layer;

	private List<HealthController> healthControllers;

	// Start is called before the first frame update
	void Start()
	{
		healthControllers = new List<HealthController>();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void StartAttack()
	{
		Debug.Log("Starting attack");
		healthControllers.Clear();
	}

	void EndAttack()
	{
		Debug.Log("Ending attack");
	}

	void GiveDamage()
	{
		foreach (HealthController item in healthControllers)
		{
			item.Damage(damageAmt);
			Debug.Log($"{this.gameObject.name} damaged {item.name} by {damageAmt} dmg");
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (isAttacking)
		{
			if (collision.gameObject.layer == layer)
			{
				if(collision.gameObject.TryGetComponent<HealthController>(out HealthController hitHealthController))
				{
					if (!healthControllers.Contains(hitHealthController))
					{
						healthControllers.Add(hitHealthController);
					}
				}
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (isAttacking)
		{
			if (other.gameObject.layer == layer)
			{
				if (other.TryGetComponent<HealthController>(out HealthController hitHealthController))
				{
					if (!healthControllers.Contains(hitHealthController))
					{
						healthControllers.Add(hitHealthController);
					}
				}
			}
		}
	}
}
