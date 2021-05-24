using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HealthV2;
using EventSystem;

public class AudioManager : MonoBehaviour
{
	List<AudioDataSO> audios = new List<AudioDataSO>();
	// Start is called before the first frame update
	void Start()
	{
		GameEvents.current.combat.OnAttack += OnAttack;
	}

	// Update is called once per frame
	void Update()
	{
		foreach (AudioDataSO item in audios)
		{
			item.Update();
		}
	}

	private void OnAttack(GameObject target, GameObject source, Attack attack, AttackResponse attackResponse)
	{
		if(attackResponse.HitType == AttackResponse.HitResult.Light)
		{
			PlaySound("light_hit");
		}
	}

	private void OnWarning(AttackWarning attackWarning)
	{
		if(attackWarning.Type == AttackWarning.WarningType.StartCountering)
		{
			PlaySound("counter_window");
		}
	}

	private void PlaySound(string soundName)
	{
		AudioDataSO audioData = Resources.Load<AudioDataSO>("SFX/" + soundName);
		audioData.Play();
		audios.Add(audioData);
	}
}
