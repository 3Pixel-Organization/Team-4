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
		Player.levelSystem.OnLevelChange += OnPlayerLevelUp;
		Player.levelSystem.OnExpChange += OnPlayerExpChange;
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
		expForNextLvl = Exp.ExpForLvl(Player.level);
		expBar.maximum = Mathf.RoundToInt(expForNextLvl);
		expBar.current = Mathf.RoundToInt(Player.expPoints);
		expBar.minimum = 0;//Mathf.RoundToInt(GetExpForLevel(Player.level));
		expBar.ValueChange();
	}

	private void SetupLevelText()
	{
		currentLevelText.SetText("lvl: " + Player.level);
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
			expBar.current = Mathf.RoundToInt(Player.expPoints);
			expBar.ValueChange();
		}
	}

	private void OnDestroy()
	{
		Player.levelSystem.OnLevelChange -= OnPlayerLevelUp;
		Player.levelSystem.OnExpChange -= OnPlayerExpChange;
	}
}
