using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Health;
using EventSystem;
using UnityEngine.VFX;

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
				//GiveDamage();
				EndAttack();
			}
		}
	}

	public int damageAmt;

	[SerializeField] private int layer;
	[SerializeField] private bool hasTrail;
	[SerializeField] private bool hasEffect;
	[SerializeField] private ParticleSystem particleSystem;
	[SerializeField] private VisualEffect visualEffect;

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
		if (hasTrail)
		{
			particleSystem.Play();
		}
		if (hasEffect)
		{
			visualEffect.Play();
		}
	}

	void EndAttack()
	{
		Debug.Log("Ending attack");
		if (hasTrail)
		{
			particleSystem.Stop(false, ParticleSystemStopBehavior.StopEmitting);
		}
		if (hasEffect)
		{
			visualEffect.Stop();
		}
		if (healthControllers.Count > 0)
		{
			PlayerManager playerManager = gameObject.GetComponentInParent<PlayerManager>();
			if (playerManager != null)
			{
				for (int i = 0; i < healthControllers.Count; i++)
				{
					GameEvents.current.player.DamageEnemy();
				}
			}
		}
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
			if (collision.gameObject.layer != gameObject.layer)
			{
				if(collision.gameObject.TryGetComponent<HealthController>(out HealthController hitHealthController))
				{
					if (!healthControllers.Contains(hitHealthController))
					{
						healthControllers.Add(hitHealthController);
						hitHealthController.Damage(damageAmt);
					}
				}
			}
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		if (isAttacking)
		{
			if (collision.gameObject.layer != gameObject.layer)
			{
				if (collision.gameObject.TryGetComponent<HealthController>(out HealthController hitHealthController))
				{
					if (!healthControllers.Contains(hitHealthController))
					{
						healthControllers.Add(hitHealthController);
						hitHealthController.Damage(damageAmt);
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
						hitHealthController.Damage(damageAmt);
					}
				}
			}
		}
	}

	private void OnTriggerStay(Collider other)
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
						hitHealthController.Damage(damageAmt);
					}
				}
			}
		}
	}
}
