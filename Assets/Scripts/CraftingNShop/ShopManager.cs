using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopActive;
    public GameObject shopInactive;


    public CustomerManager customerManager;
    bool hitLever;

    //public bool shopOpen;

    private void Start()
    {
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
            
        }

        if (!hitLever)
        {
            customerManager.shopOpen = true;

            shopInactive.SetActive(false);
            shopActive.SetActive(true);
        }
    }

    
}
