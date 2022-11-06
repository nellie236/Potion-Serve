using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerActions : MonoBehaviour
{
    public float walkSpeed = 3f;
    private Rigidbody2D myRB;

    public bool spokenTo;
    public bool waiting;
    public bool accepted;
    public bool correctItem;
    public bool atShop;
    int triggers;
    public static int relationship;
    public static int shopVisits;

    private void Start()
    {
        triggers = 0;
        myRB = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("ShopTrigger")))
        {
            triggers += 1;

            if (triggers >= 2) 
            { 
                atShop = true;
                shopVisits += 1;
            }
        }
    }

    private void Update()
    {
        if (atShop == false)
        {
            myRB.velocity = new Vector2(walkSpeed, 0);
            return;
        }
        
        if (atShop == true)
        {
            if (spokenTo == false)
            {
                waiting = true;

                if (waiting == true)
                {
                    //run waiting timer, when timer is done, leave. -relationship int
                }
            }

            if (spokenTo == true)
            {
                waiting = false;

                if (accepted == false)
                {
                    //leave, -relationship int
                }

                if (accepted == true)
                {
                    if (correctItem == false)
                    {
                        //leave, -relationship int
                    }

                    if (correctItem == true)
                    {
                        // +relationship int, leave
                    }
                }
            }
        }

    }

}
