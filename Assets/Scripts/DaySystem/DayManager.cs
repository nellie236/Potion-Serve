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

    //int dailyNPCAmount;
    //public List<GameObject> Customers;
    //int whichNPC;
    //int customersPresent = 0;

    //public List<GameObject> currentCustomers = new List<GameObject>();

    void Start()
    {
        ppv = GameObject.Find("Global Volume").GetComponent<Volume>();
        dayDisplayCount.text = "Day " + dayCount;
        DailyDecisions();
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

    public void DisplayTime()
    {
        timeDisplay.text = string.Format("{0:00}:{1:00}", hours, mins);
    }

    public void NextDay()
    {
        dayCount++;
        dayDisplayCount.text = "Day " + dayCount;
        //DailyDecisions();
    }

    public void DailyDecisions()
    {
        //here will go daily decisions, like deciding how many npcs will show up to the shop, and which ones CAN show up according to index / chance
        //dailyNPCAmount = Random.Range(1, Customers.Count);
        
        /*for (customersPresent = 0; customersPresent < dailyNPCAmount; customersPresent++)
        {
            whichNPC = Random.Range(0, Customers.Count);
            SpawnCertainCustomer();
            Customers.RemoveAt(whichNPC);
            //chose the NPC, instantiate the NPC, decide which NPC will come first, remove that NPC from spawnable list
            //to decide which is first, have int that gets added too at the end of each loop. whichever one is "first" has the lowest value, the others are told to wait a certain amount of time
        }
        //for each dailyNPCAmount, choose random available NPC, remove NPC from the availablelist, and choose whether they'll go first /second etc for (i = 0; i < dailyNPCAmount; i++)
        
        foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
        {
            currentCustomers.Add(npc);
        }*/

    }

    /*public void SpawnCertainCustomer()
    {
        switch (whichNPC)
        {
            case 0:
                    GameObject NPC0 = Instantiate(Customers[0]) as GameObject;
                break;
            case 1:
                    GameObject NPC1 = Instantiate(Customers[1]) as GameObject;
                break;
            case 2:
                    GameObject NPC2 = Instantiate(Customers[2]) as GameObject;
                break;
            case 3:
                    GameObject NPC3 = Instantiate(Customers[3]) as GameObject;
                break;
            case 4:
                    GameObject NPC4 = Instantiate(Customers[4]) as GameObject;
                break;
        }
    }*/

    

    
}
