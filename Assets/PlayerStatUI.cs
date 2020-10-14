using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUI : MonoBehaviour
{
	[SerializeField] private ProgressBar expBar;
	[SerializeField] private TMPro.TextMeshProUGUI currentLevelText;


	float expForNextLvl;

	// Start is called before the first frame update
	void Start()
	{
		GameEvents.current.OnPlayerLevelUp += OnPlayerLevelUp;
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
		if(expBar != null)
		{
			expBar.current = Mathf.RoundToInt(Player.expPoints);
		}
	}

	private void SetupExpBar()
	{
		expForNextLvl = GetExpForLevel(Player.level + 1);
		expBar.maximum = Mathf.RoundToInt(expForNextLvl);
		expBar.current = Mathf.RoundToInt(Player.expPoints);
		expBar.minimum = Mathf.RoundToInt(GetExpForLevel(Player.level));

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

	public static float GetExpForLevel(int level)
	{
		float returnValue = (float)level * 1.2f * 100f;
		return returnValue;
	}
}
