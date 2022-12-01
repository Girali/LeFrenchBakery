using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DUI_Timer : MonoBehaviour
{
    public Image cookTimer;
    public Image overCookTimer;

    private float timerStart;
    private float timerEnd1;
    private float timerEnd2;
    private float timerDuration1;
    private float timerDuration2;

    public void StartTimer(float cookingTime, float overCookedTime)
    {
        timerStart = Time.time;
        timerDuration1 = cookingTime;
        timerDuration2 = overCookedTime;
        timerEnd1 = timerStart + timerDuration1;
        timerEnd2 = timerStart + timerDuration1 + timerDuration2;
    }

    private void Update()
    {
        if(Time.time < timerEnd1)
        {
            cookTimer.fillAmount = (Time.time - timerStart) / (timerEnd1 - timerStart);
        }
        else if(Time.time < timerEnd2) 
        {
            cookTimer.color = Color.green;
            cookTimer.fillAmount = 1;
            overCookTimer.fillAmount = (Time.time - timerEnd1) / (timerEnd2 - timerEnd1);
        }
        else
        {
            cookTimer.fillAmount = 1;
            overCookTimer.fillAmount = 1;

            cookTimer.color = Color.black;
            overCookTimer.color = Color.black;
        }
    }
}
