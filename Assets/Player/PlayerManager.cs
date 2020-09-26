using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	[SerializeField] private Weapon currentWeapon;
	[SerializeField] private WeaponHandler weaponHandler;

	// Start is called before the first frame update
	void Start()
	{
		weaponHandler.SetupWeapon(currentWeapon);
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Loot"))
		{
			LootDrop lootDrop = other.GetComponent<LootDrop>();
			Inventory.AddItemToInventory(lootDrop.item);
			Destroy(other.gameObject, 0.01f);
			Debug.Log(Inventory.items[0].name);
		}
	}
}
