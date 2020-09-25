using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour
{
	public Item item;

	[SerializeField] private TMPro.TextMeshProUGUI itemText;

	public void SetupLoot(Item item)
	{
		this.item = item;
		itemText.SetText(item.name + " - lvl " + item.level);
		switch (item.rarity)
		{
			case ItemRarity.Trash:
				itemText.color = Color.gray;
				break;
			case ItemRarity.Epic:
				itemText.color = Color.magenta;
				break;

			case ItemRarity.Dev:
				itemText.color = Color.white;
				break;
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
