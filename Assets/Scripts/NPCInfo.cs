using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInfo : MonoBehaviour
{
    [SerializeField]
    public string characterName;
    [SerializeField]
    public Image characterSprite;
    [SerializeField]
    public Image dialogueProfile;

    [SerializeField]
    public float timeWaiting;
    [SerializeField]
    public string request;
    [SerializeField]
    public int amountPay;


    public float walkSpeed = 3f;

    private Rigidbody2D myRigidbody;

    public bool isWalking;

    public float walkTime;
    private float walkCounter;
    public float waitTime;
    private float waitCounter;

    private int WalkDirection;

    public bool atShop;

    public bool finishedTalking;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        waitCounter = waitTime;
        walkCounter = walkTime;

        atShop = false;
    }

    private void Update()
    {
        if (atShop == false)
        {
            WalkDirection = 0;
            isWalking = true;
        }
        else if ((atShop == true) && (finishedTalking == true))
        {
            WalkDirection = 1;
            isWalking = true;
        }

        if (isWalking == true)
        {
            walkCounter -= Time.deltaTime;

            if (walkCounter < 0)
            {
                //atShop = true;
                isWalking = false;
                waitCounter = waitTime;
            }

            switch (WalkDirection)
            {
                case 0: //move towards the shop, right
                    myRigidbody.velocity = new Vector2(walkSpeed, 0);
                    break;
                case 1: //move away from the shop, left
                    myRigidbody.velocity = new Vector2(-walkSpeed, 0);
                    break;
            }
        }
        else if (isWalking == false)
        {
            waitCounter -= Time.deltaTime;

            if (waitCounter < 0)
            {
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ShopTrigger"))
        {
            atShop = true;
        }
    }

}
