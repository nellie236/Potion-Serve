using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    public static int dayCount;
    public Text dayDisplayCount;
    private void Start()
    {
        dayDisplayCount.text = "Day " + dayCount;
    }

    public void NextDay()
    {
        dayCount++;
        dayDisplayCount.text = "Day " + dayCount;
    }

    
}
