using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCActions : MonoBehaviour
{
    public float walkSpeed = 3f;
    private Rigidbody2D myRB;
    public DayManager dayManager;
    private bool walkToShop;

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        dayManager = (DayManager) GameObject.FindObjectOfType(typeof(DayManager));

        /*if (dayManager.currentCustomers.IndexOf(this.gameObject) == 1)
        {
            WalkToShop();
        }*/

        WalkToShop();
    }

    private void WalkToShop()
    {
        walkToShop = true;
        
        
        //bool that says npc talked to player
        //waiting for request
        //told yes or no, leaving
        //tell static list of decisions what the player did / did not give npc
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ShopTrigger")
        { walkToShop = false; }
    }

    private void LeaveShop()
    {
        myRB.velocity = new Vector2(-walkSpeed, 0);
        //"destroy" object
        //tell next customer, if there is, to walktoShop
    }

    private void Update()
    {
       if (walkToShop == true)
        {
            myRB.velocity = new Vector2(walkSpeed, 0);
        }
    }
}
