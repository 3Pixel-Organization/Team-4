using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Health;
using UnityEngine.VFX;
using HealthV2;

public class Enemy : HealthSystem
{
	public int level;
	public TMPro.TextMeshProUGUI levelText;
	public EnemyPrefab enemyPrefab;

	public VisualEffect deathEffect;
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

	protected override void Death()
	{
		base.Death();
		//GameManager.current.EnemyDeath(transform, this);
		
		//deathEffect.Play();
		//audioSource.pitch = Random.Range(0.5f, 2);
		//audioSource.Play();
		Destroy(this.gameObject);
	}
}
