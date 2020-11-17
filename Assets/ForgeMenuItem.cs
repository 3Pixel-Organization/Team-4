using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForgeMenuItem : MonoBehaviour
{
	[SerializeField] private Image itemImg;
	[SerializeField] private TextMeshProUGUI itemText;

	private Item item;

	public void SetupItem(Item item)
	{
		this.item = item;
		itemImg.sprite = item.sprite;
		itemText.SetText($"{item.name} - {item.level}");
		itemText.color = ItemManager.GetRarityColor(item.rarity);
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
