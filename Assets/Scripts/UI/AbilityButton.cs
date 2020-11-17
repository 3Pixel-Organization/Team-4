using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
	[SerializeField] private int abilityID;

	[SerializeField] private Image buttonImg;

	[SerializeField] private Color cooldownColor;

	// Start is called before the first frame update
	void Start()
	{
		GameEvents.current.onAbilityCooldownStart += CooldownStart;
		GameEvents.current.onAbilityCooldownEnd += CooldownEnd;
	}

	private void OnDestroy()
	{
		GameEvents.current.onAbilityCooldownStart -= CooldownStart;
		GameEvents.current.onAbilityCooldownEnd -= CooldownEnd;
	}

	void CooldownStart(int id)
	{
		if(abilityID == id)
		{
			buttonImg.color = cooldownColor;
		}
	}

	void CooldownEnd(int id)
	{
		if (abilityID == id)
		{
			buttonImg.color = Color.white;
		}
	}
}
