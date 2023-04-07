using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildSpot : MonoBehaviour
{
    public Transform mySpot;
    public GameObject spawnedDispenser;
    public MerchantItem currentItem;
    public MerchantItem heldItem;
    MerchantManager merchantManager;
    public Image currentImage;
    public Sprite originalImage;

    // Start is called before the first frame update
    void Start()
    {
        merchantManager = GameObject.Find("Merchant").GetComponent<MerchantManager>();
        currentImage = GetComponent<Image>();
        if (currentItem != null)
        {
            onClick();
        }

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
            if (merchantManager.selectedItem != null)
            {
                if (merchantManager.selectedItem.dispenser)
                {
                    currentItem = merchantManager.selectedItem;
                }
            }

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
                currentImage.sprite = currentItem.dispenserBuildIcon;
                
            }
        }
        else if (merchantManager.placed == 1)
        {
            if (heldItem != null && spawnedDispenser != null)
            {
                Destroy(spawnedDispenser);
                heldItem = null;
                merchantManager.placed = 0;
                currentImage.sprite = originalImage;
                //pick up and move
            }
        }
    }
}
