using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Health;

public class Enemy : MonoBehaviour
{
	public int level;
	public TMPro.TextMeshProUGUI levelText;

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
		if (Random.Range(0, 1) <= 1)
		{
			GameManager.current.SpawnLoot(transform.position, ItemManager.CreateArmor("iron Dev", 20, 1000, ItemRarity.Epic, "IronHelmet", 10, new Enchantment(EnchantmentType.None)));
		}
		Destroy(this.gameObject);
	}
}
