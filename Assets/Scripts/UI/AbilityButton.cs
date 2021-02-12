using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem;

public class AbilityButton : MonoBehaviour
{
	[SerializeField] private int abilityID;

	[SerializeField] private Image buttonImg;

	[SerializeField] private Color cooldownColor;

	// Start is called before the first frame update
	void Start()
	{
		GameEvents.current.player.OnAbilityCooldownStart += CooldownStart;
		GameEvents.current.player.OnAbilityCooldownEnd += CooldownEnd;
	}

	private void OnDestroy()
	{
		GameEvents.current.player.OnAbilityCooldownStart -= CooldownStart;
		GameEvents.current.player.OnAbilityCooldownEnd -= CooldownEnd;
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
