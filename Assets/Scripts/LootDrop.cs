using Health;
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
		GameObject box = Instantiate(item.model, itemSpawnPos);
		Damage dmgBox = box.GetComponentInChildren<Damage>();
		Instantiate(ItemManager.GetRarityBox(item.rarity), transform);
		if(dmgBox != null)
		{
			dmgBox.enabled = false;
		}
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
