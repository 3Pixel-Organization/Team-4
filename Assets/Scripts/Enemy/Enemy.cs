﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Health;
using UnityEngine.VFX;
using HealthV2;

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
	// Start is called before the first frame update
	void Start()
	{
		//healthController = GetComponent<HealthController>();
		//healthController.Death += Death;
		InstanceHealthSystem(100);
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public override void Damage(float damage)
	{
		if(enemyState == EnemyState.Normal)
		{
			base.Damage(damage);
		}
		else if(enemyState == EnemyState.Vulnerable)
		{
			base.Damage(damage * 2);
		}
		else if(enemyState == EnemyState.Guard)
		{
			base.Damage(damage * 0.1f);
		}
	}
	

	protected override void Death()
	{
		base.Death();
		//GameManager.current.EnemyDeath(transform, this);

		//deathEffect.Play();
		//audioSource.pitch = Random.Range(0.5f, 2);
		//audioSource.Play();
		GameObject deathEff = Instantiate(deathEffect, transform.position + Vector3.up * 1.2f , transform.rotation);
		Destroy(deathEff, 1.5f);
		Destroy(this.gameObject);
	}
}
