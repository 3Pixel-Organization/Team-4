using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTracker : MonoBehaviour
{
	private float time;
	private bool timerIsActive;

	public int Hours 
	{
		get{
			return Mathf.FloorToInt(time / 60 / 60);
		}
	}

	private void Start()
	{
		GameEvents.current.OnLevelStart += TimerStart;
		GameEvents.current.OnLevelEnd += TimerEnd;
	}

	private void Update()
	{
		if(timerIsActive)
		{
			time += Time.deltaTime;
		}
	}

	private void TimerStart()
	{
        timerIsActive = true;

    }
	

	private void TimerEnd()
	{
        timerIsActive = false;

	}

}
