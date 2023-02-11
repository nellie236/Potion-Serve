using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject onLever;
    public GameObject offLever;
    //bool inTrigger;

    public GameObject openSign;
    public GameObject openLight;
    public GameObject closedSign;
    public CustomerManager customerManager;
    bool hitLever;

    //public bool shopOpen;

    private void Start()
    {
        customerManager = GameObject.Find("CustomerManager").GetComponent<CustomerManager>();
        SwitchOpenClose();
    }

    public void SwitchOpenClose()
    {
        hitLever = !hitLever;

        if (hitLever)
        {
            customerManager.shopOpen = false;

            //shopOpen = false; 
            onLever.SetActive(false);
            openLight.SetActive(false);
            closedSign.SetActive(true);
            offLever.SetActive(true);
        }

        if (!hitLever)
        {
            customerManager.shopOpen = true;

            //shopOpen = true;
            onLever.SetActive(true);
            openLight.SetActive(true);
            closedSign.SetActive(false);
            offLever.SetActive(false);
        }
    }

    
}
