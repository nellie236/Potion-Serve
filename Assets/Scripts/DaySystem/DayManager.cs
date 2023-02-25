using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering; 

public class DayManager : MonoBehaviour
{
    public static int dayCount = 1;
    public Text dayDisplayCount;
    public Text timeDisplay;
    public Volume ppv; //this is the post processing volume

    public float tick; //increasing the tick, increases second rate
    public float seconds;
    public int mins;
    public int hours;

    public GameObject arrowRotate;
    public float rotationSpeed;

    void Start()
    {
        Time.timeScale = 1;
        ppv = GameObject.Find("Global Volume").GetComponent<Volume>();
        dayDisplayCount.text = "Day " + dayCount;
    }

    void FixedUpdate()
    {
        CalculateTime();
        DisplayTime();
    }

    public void CalculateTime() //used to calc sec, min, hours
    {
        seconds += Time.fixedDeltaTime * tick; //multiply time between fixed update by tick
        //Debug.Log(seconds);

        if (seconds >= 60) //60 sec = 1 min 
        {
            seconds = 0;
            mins += 1;
        }

        if (mins >= 60)
        {
            mins = 0;
            hours += 1;
        }

        if (hours >= 12)
        {
            hours = 0;
            NextDay();
        }
        ControlPPV();
        RotateClockArrow();
    }

    public void ControlPPV()
    {
        if (hours >= 5 && hours < 6) //dusk at 21:00 / 9 pm - until 22:00 / 10pm
        {
            ppv.weight = (float)mins / 60; //since dusk is 1 hr, we divide the mins by 60 which will slowly increase 0-1
      
        }

        if (hours >= 11 && hours < 12)
        {
            ppv.weight = 1 - (float)mins / 60; //we minus 1 because we want it to go from 1 - 0
            
        }   
    }

    public void RotateClockArrow()
    {
        if (hours >= 0 && hours < 6)
        {
            arrowRotate.transform.Rotate(0, 0, Time.deltaTime * -rotationSpeed);
        }
        if (hours >= 6 && hours < 12)
        {
            arrowRotate.transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
        }
    }

    public void DisplayTime()
    {
        timeDisplay.text = string.Format("{0:00}:{1:00}", hours, mins);
    }

    public void NextDay()
    {
        dayCount++;
        dayDisplayCount.text = "Day " + dayCount;
        
    }

    

    
}
