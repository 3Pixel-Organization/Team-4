using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboUI : MonoBehaviour
{
	[SerializeField] private TMPro.TextMeshProUGUI currentComboText;
	[SerializeField] private TMPro.TextMeshProUGUI currentMaxComboText;
	[SerializeField] private ComboHandler comboHandler;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if (comboHandler.ComboIsActive)
		{
			currentComboText.SetText(comboHandler.comboCount + "");
		}
		else
		{
			currentComboText.SetText("0");
		}
	}
}
