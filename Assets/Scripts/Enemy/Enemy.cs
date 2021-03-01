using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Health;
using UnityEngine.VFX;

public class Enemy : MonoBehaviour
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
		healthController = GetComponent<HealthController>();
		healthController.Death += Death;
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void Death()
	{
		//GameManager.current.EnemyDeath(transform, this);
		
		deathEffect.Play();
		audioSource.pitch = Random.Range(0.5f, 2);
		audioSource.Play();
		Destroy(this.gameObject);
	}
}
