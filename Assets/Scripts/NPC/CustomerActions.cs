using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerActions : MonoBehaviour
{
    public float walkSpeed = 3f;
    private Rigidbody2D myRB;

    public GameObject desiredItem;

    public bool spokenTo;
    public bool waiting;
    public bool accepted;
    public bool denied;
    public bool correctItem;
    public bool wrongItem;
    public bool atShop;
    public bool voidTrigger;
    public bool finishedDialogue;
    public int triggers;
    public static int relationship;
    public static int shopVisits;
    public float patienceTime;
    
    

    private void Start()
    {
        triggers = 0;
        voidTrigger = false;
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
            }
        }

        if (collision.CompareTag("VoidTrigger"))
        {
            atShop = false;
            voidTrigger = true;
            waiting = false;
        }

        if (collision.CompareTag("DoneAtShop"))
        {
            if (finishedDialogue == true)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void Update()
    {
        if (GameObject.Find("ShopManagerObject").GetComponent<ShopManager>().shopOpen == false)
        {
            if (voidTrigger == false)
            {
                waiting = false;
                triggers = 0;
                Leave();
            }

            return;
        }
        if (GameObject.Find("ShopManagerObject").GetComponent<ShopManager>().shopOpen == true)
        {
            if (atShop == false && patienceTime >= 0f)
            {
                voidTrigger = false;
                Enter();
                return;
            }

            if (atShop == true)
            {
                if (spokenTo == false && voidTrigger == false && triggers >= 2)
                {
                    waiting = true;
                }

                if (spokenTo == true)
                {
                    shopVisits += 1;
                    waiting = false;

                    GetComponentInChildren<DialogueTrigger>().whichMessages = 0;

                    if (accepted == false && denied == true)
                    {
                        GetComponentInChildren<DialogueTrigger>().whichMessages = 2;
                        //leave, -relationship int
                    }

                    if (accepted == true && denied == false)
                    {
                        GetComponentInChildren<DialogueTrigger>().whichMessages = 1;

                        if (correctItem == false && wrongItem == true)
                        {
                            GetComponentInChildren<DialogueTrigger>().whichMessages = 4;
                            //leave, -relationship int

                        }

                        if (correctItem == true && wrongItem == false)
                        {
                            GetComponentInChildren<DialogueTrigger>().whichMessages = 3;
                            
                            // +relationship int, leave
                        }
                    }
                }
            }

            if (finishedDialogue == true)
            {
                patienceTime = -1;
                Leave();
            }

            if (waiting == true)
            {
                //run waiting timer, when timer is done, leave. -relationship int
                patienceTime -= Time.deltaTime;

                if (patienceTime <= 0.0f && voidTrigger == false)
                {
                    atShop = false;
                    //triggers = 0;
                    Leave();
                }
            }
        }

    }

    public void CorrectItem()
    {
        correctItem = true;
        wrongItem = false;
    }


    public void WrongItem()
    {
        wrongItem = true;
        correctItem = false;
    }

    public void Enter()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        myRB.velocity = new Vector2(walkSpeed, 0);
    }

    public void Leave()
    {
        //atShop = false;
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        myRB.velocity = new Vector2(-walkSpeed, 0);
    }
}
