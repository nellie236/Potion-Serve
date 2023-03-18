using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopActive;
    public GameObject shopInactive;


    public CustomerManager customerManager;
    Animator animator;
    bool hitLever;

    //public bool shopOpen;

    private void Start()
    {
        animator = GetComponent<Animator>();
        shopActive = GameObject.Find("ShopOn");
        shopInactive = GameObject.Find("LeverOff");
        customerManager = GameObject.Find("CustomerManager").GetComponent<CustomerManager>();
        SwitchOpenClose();
    }

    public void SwitchOpenClose()
    {
        hitLever = !hitLever;

        if (hitLever)
        {
            customerManager.shopOpen = false;

            shopActive.SetActive(false);
            shopInactive.SetActive(true);
            animator.SetBool("hitLever", false);
            animator.SetBool("switchedOn", false);
            
        }

        if (!hitLever)
        {
            customerManager.shopOpen = true;

            shopInactive.SetActive(false);
            shopActive.SetActive(true);
            animator.SetBool("hitLever", true);
            animator.SetBool("switchedOn", true);
        }
    }

    

    
}
