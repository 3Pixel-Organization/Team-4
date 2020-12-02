using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem;


public bool isTimerRunning =  false;
public float time;
public class TimeTracker : MonoBehaviour

{
	void Start(){
        GameEvents.current.LevelEvents.OnLevelStart += TimerStart;
        GameEvents.current.LevelEvents.OnLevelEnd  += TimerEnd;    
    }
    void TimerStart(){
        isTimerRunning = true;
    }
    void Update(){
        if(isTimerRunning){
            time = Time.deltatime;
        }

    }
    void TimerEnd(){
        isTimerRunning = false;
    }


}
