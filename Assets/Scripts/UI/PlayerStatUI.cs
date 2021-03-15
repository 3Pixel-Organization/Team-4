using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Levels;

public class PlayerStatUI : MonoBehaviour
{
	[SerializeField] private ProgressBar expBar;
	[SerializeField] private TMPro.TextMeshProUGUI currentLevelText;


	float expForNextLvl;

	// Start is called before the first frame update
	void Start()
	{
		PlayerData.levelSystem.OnLevelChange += OnPlayerLevelUp;
		PlayerData.levelSystem.OnExpChange += OnPlayerExpChange;
		if(expBar != null)
		{
			SetupExpBar();
		}
		if(currentLevelText != null)
		{
			SetupLevelText();
		}
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void SetupExpBar()
	{
		expForNextLvl = Exp.ExpForLvl(PlayerData.level);
		expBar.maximum = Mathf.RoundToInt(expForNextLvl);
		expBar.current = Mathf.RoundToInt(PlayerData.expPoints);
		expBar.minimum = 0;//Mathf.RoundToInt(GetExpForLevel(Player.level));
		expBar.ValueChange();
	}

	private void SetupLevelText()
	{
		currentLevelText.SetText("lvl: " + PlayerData.level);
	}

	private void OnPlayerLevelUp()
	{
		if(expBar != null)
		{
			SetupExpBar();
		}
		if(currentLevelText != null)
		{
			SetupLevelText();
		}
	}

	private void OnPlayerExpChange()
	{
		if (expBar != null)
		{
			expBar.current = Mathf.RoundToInt(PlayerData.expPoints);
			expBar.ValueChange();
		}
	}

	private void OnDestroy()
	{
		PlayerData.levelSystem.OnLevelChange -= OnPlayerLevelUp;
		PlayerData.levelSystem.OnExpChange -= OnPlayerExpChange;
	}
}
