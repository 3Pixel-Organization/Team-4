using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemHandler : MonoBehaviour
{
	[SerializeField] private Image itemImage;
	[SerializeField] private TMPro.TextMeshProUGUI itemText;
	[SerializeField] private Button button;


	public void SetupItem(Item item)
	{
		itemText.SetText(item.name);
		itemImage.sprite = item.sprite;
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
