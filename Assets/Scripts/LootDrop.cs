using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour
{
	public Item item;

	[SerializeField] private Transform itemSpawnPos;

	[SerializeField] private TMPro.TextMeshProUGUI itemText;

	public void SetupLoot(Item item)
	{
		this.item = item;
		itemText.SetText(item.name + " - lvl " + item.level);
		itemText.color = ItemManager.GetRarityColor(item.rarity);
		Instantiate(item.model, itemSpawnPos);
	}
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
