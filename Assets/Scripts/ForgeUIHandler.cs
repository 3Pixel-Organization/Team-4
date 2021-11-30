using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeUIHandler : MonoBehaviour
{
	[SerializeField] private GameObject itemListParent;
	[SerializeField] private GameObject itemUIPrefab;

	private List<GameObject> currentItemsList;
	// Start is called before the first frame update
	void Start()
	{
		currentItemsList = new List<GameObject>();
		foreach (Item item in Inventory.items)
		{
			GameObject instItem = Instantiate(itemUIPrefab, itemListParent.transform);
			instItem.GetComponent<ForgeMenuItem>().SetupItem(item);
			currentItemsList.Add(instItem);
		}

		RectTransform itemListParentRect = itemListParent.GetComponent<RectTransform>();
		itemListParentRect.sizeDelta = new Vector2(currentItemsList.Count * 300 - 1860, itemListParentRect.rect.height);
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
