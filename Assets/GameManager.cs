using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject LootPrefab;

	// Start is called before the first frame update
	void Start()
	{
		Item katana = new Item("Catana", 99, ItemRarity.Epic);
		SpawnLoot(new Vector3(1, 1, 1), katana);
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void SpawnLoot(Vector3 position, Item item)
	{
		GameObject gameLoot = Instantiate(LootPrefab, position, Quaternion.identity);
		gameLoot.GetComponent<LootDrop>().SetupLoot(item);
	}
}
