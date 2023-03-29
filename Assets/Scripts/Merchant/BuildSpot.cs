using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSpot : MonoBehaviour
{
    public Transform mySpot;
    public GameObject spawnedDispenser;
    public bool occupied; //if occupied and clicked then switch out
    public MerchantItem currentItem;
    public MerchantItem heldItem;
    MerchantManager merchantManager;

    // Start is called before the first frame update
    void Start()
    {
        merchantManager = GameObject.Find("Merchant").GetComponent<MerchantManager>();
        occupied = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        //if merchant.int = 0, place, else if > 0, don't? switch back and forth between 0 and 1. 

        if (merchantManager.placed == 0)
        {
            currentItem = merchantManager.selectedItem;

            if (heldItem != null)
            {
                //if hand is empty, PICK UP what is in the spot. if hand has an item in it, instantiate and occupied = true; 
            }
            else if (heldItem == null)
            {
                GameObject newDispenser = Instantiate(currentItem.dispenser, new Vector2(mySpot.transform.position.x, mySpot.transform.position.y), Quaternion.identity) as GameObject;
                newDispenser.transform.localPosition = mySpot.transform.position;
                merchantManager.placed = 1;
                spawnedDispenser = newDispenser;
                heldItem = currentItem;
            }
        }
        else if (merchantManager.placed == 1)
        {
            if (heldItem != null && spawnedDispenser != null)
            {
                Destroy(spawnedDispenser);
                heldItem = null;
                merchantManager.placed = 0;
                //pick up and move
            }
        }
    }
}
