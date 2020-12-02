using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTracker : MonoBehaviour
{
	private float time;
	private bool timerIsActive;

	public int Minutes 
	{
		get
        {
			return Mathf.FloorToInt(time / 60);
		}
	}
    public int Seconds{
        get
        {
            return (int)time % 60;
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

	public void TimerStart()
	{
		timerIsActive = true;

	}
	

	public void TimerEnd()
	{
		timerIsActive = false;

	}

}
