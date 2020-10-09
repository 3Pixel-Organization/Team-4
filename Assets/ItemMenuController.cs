using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenuController : MonoBehaviour
{
	[SerializeField] private GameObject menuItemPrefab;

	// Start is called before the first frame update
	void Start()
	{
		Inventory.Load();
		foreach (Item item in Inventory.items)
		{
			GameObject itemGameObj = Instantiate(menuItemPrefab, transform);
			itemGameObj.GetComponent<MenuItemHandler>().SetupItem(item);
		}
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
