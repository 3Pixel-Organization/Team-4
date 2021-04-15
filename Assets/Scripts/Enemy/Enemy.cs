using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Health;
using UnityEngine.VFX;
using HealthV2;
using UnityEngine.AI;

public class Enemy : HealthSystem
{
	public enum EnemyState
	{
		Normal,
		Guard,
		Vulnerable
	}

	public EnemyState enemyState;

	public int level;
	public TMPro.TextMeshProUGUI levelText;
	public EnemyPrefab enemyPrefab;

	public GameObject deathEffect;
	public AudioSource audioSource;

	private HealthController healthController;

	private Animator animator;

	private NavMeshAgent agent;
	// Start is called before the first frame update
	void Start()
	{
		//healthController = GetComponent<HealthController>();
		//healthController.Death += Death;
		InstanceHealthSystem(15);
		animator = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		TakeDamage += TookDamage;
	}

	// Update is called once per frame
	void Update()
	{
		animator.SetFloat("MoveSpeed", agent.desiredVelocity.magnitude * 0.2f);
	}

	public override AttackResponse Damage(Attack attack)
	{
		if (!IsAlive) return new AttackResponse(0, AttackResponse.HitResult.None);
		OnAttacked(attack);

		AttackResponse attackResponse = OnReponseToAttack(attack);
		if(attackResponse.HitType == AttackResponse.HitResult.Blocked)
		{
			return attackResponse;
		}

		if (attackResponse.DamageTaken > 0)
		{
			BuiltInDamage(attackResponse.DamageTaken);
		}
		return attackResponse;
	}
	

	protected override void Death()
	{
		base.Death();
		//GameManager.current.EnemyDeath(transform, this);

		//deathEffect.Play();
		//audioSource.pitch = Random.Range(0.5f, 2);
		//audioSource.Play();

		animator.SetTrigger("Death");

		GameObject deathEff = Instantiate(deathEffect, transform.position + Vector3.up * 1.2f , transform.rotation);
		Destroy(deathEff, 1.5f);
		Destroy(this.gameObject, 3);
	}

	void TookDamage(float currentHealth, float maxHealth, float healthDelta)
	{
		if(healthDelta > 3)
		{
			GameObject deathEff = Instantiate(deathEffect, transform.position + Vector3.up * 1.2f, transform.rotation);
			Destroy(deathEff, 1.5f);
		}
	}
}
