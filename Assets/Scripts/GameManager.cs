using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject LootPrefab;

	public List<Item> itemsToSpawn;

	// Start is called before the first frame update
	void Start()
	{
		foreach (Item item in itemsToSpawn)
		{
			SpawnLoot(new Vector3(1, 1, 1), item);
		}
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
