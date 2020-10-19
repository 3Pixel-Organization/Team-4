using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenuController : MonoBehaviour
{
	[SerializeField] private GameObject menuItemPrefab;
	[SerializeField] private ItemMenuSorce menuSorce;

	private List<GameObject> itemObjs = new List<GameObject>();
	private List<Item> displayItems = new List<Item>();


	// Start is called before the first frame update
	void Start()
	{
		ReDrawItems();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void OnEnable()
	{
		ReDrawItems();
	}

	void ReDrawItems()
	{
		switch (menuSorce)
		{
			case ItemMenuSorce.inventory:
				displayItems = Inventory.items;
				break;
			case ItemMenuSorce.currentRunItems:
				displayItems = GameManager.current.currentRunItems;
				break;
			default:
				displayItems = new List<Item>();
				break;
		}
		foreach (GameObject gameObject in itemObjs)
		{
			Destroy(gameObject);
		}
		
		foreach (Item item in displayItems)
		{
			GameObject itemGameObj = Instantiate(menuItemPrefab, transform);
			itemGameObj.GetComponent<MenuItemHandler>().SetupItem(item);
			itemObjs.Add(itemGameObj);
		}
	}
}

public enum ItemMenuSorce
{
	inventory,
	currentRunItems
}