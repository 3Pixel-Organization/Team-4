using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class ComboHandler : MonoBehaviour
{
	public int comboCount;
	float comboTimer;

	private bool comboIsActive;
	public bool ComboIsActive
	{
		get { return comboIsActive; }
		private set
		{
			comboIsActive = value;
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		GameEvents.current.player.OnDamageEnemy += PlayerEnemyHit;
		comboCount = 0;
		comboTimer = 0;
	}

	// Update is called once per frame
	void Update()
	{
		Debug.Log($"Combo count: {comboCount}");
		if(comboTimer > 0)
		{
			comboIsActive = true;
		}
		if(comboTimer < 0)
		{
			comboIsActive = false;
			comboTimer = 0;
		}
		if (comboIsActive)
		{
			comboTimer -= Time.deltaTime;
		}
	}

	void PlayerEnemyHit()
	{
		comboTimer = 2;
		comboCount++;
	}
}
