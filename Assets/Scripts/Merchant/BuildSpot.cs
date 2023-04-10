using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildSpot : MonoBehaviour
{
    public Transform mySpot;
    public GameObject spawnedDispenser;
    public MerchantItem setItem;
    public MerchantItem containedItem;
    MerchantManager merchantManager;
    public Image currentImage;
    public Sprite originalImage;

    // Start is called before the first frame update
    void Start()
    {
        merchantManager = GameObject.Find("Merchant").GetComponent<MerchantManager>();
        currentImage = GetComponent<Image>();
        if (containedItem != null)
        {
            SetDispenser();
        }
        merchantManager.placed = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDispenser()
    {
        
        GameObject newDispenser = Instantiate(containedItem.dispenser, new Vector2(mySpot.transform.position.x, mySpot.transform.position.y), Quaternion.identity) as GameObject;
        newDispenser.transform.localPosition = mySpot.transform.position;
        spawnedDispenser = newDispenser;
        currentImage.sprite = containedItem.dispenserBuildIcon;

        //merchantManager.buildMapParent.SetActive(false);
    }

    public void OnClick()
    {
            if (merchantManager.placed == 0)
            {
                StartBuild();
            }
            else if (merchantManager.placed == 1)
            {
                if (containedItem != null && spawnedDispenser != null)
                {
                    Destroy(spawnedDispenser);
                    merchantManager.buildItem = containedItem;
                    containedItem = null;
                    merchantManager.placed = 0;
                    currentImage.sprite = originalImage;
                    //pick up and move
                }
            }
    }

    public void StartBuild()
    {
        if (merchantManager.buildItem != null)
        {
            if (merchantManager.buildItem.dispenser)
            {
                setItem = merchantManager.buildItem;
                Build();
            }

            
        }
        else if (merchantManager.buildItem == null)
        {
            if (containedItem != null && spawnedDispenser != null)
            {
                Destroy(spawnedDispenser);
                merchantManager.buildItem = containedItem;
                setItem = merchantManager.buildItem;
                containedItem = null;
                merchantManager.placed = 0;
                currentImage.sprite = originalImage;
                Build();
                //pick up and move
            }

            
        }
    }

    public void Build()
    {
        if (setItem != null)
        {
            if (containedItem != null)
            {
                if (setItem != containedItem) //needs to SWAP what is in spot already with hand. set to each other
                {
                    Destroy(spawnedDispenser);
                    merchantManager.buildItem = containedItem;

                    GameObject newDispenser = Instantiate(setItem.dispenser, new Vector2(mySpot.transform.position.x, mySpot.transform.position.y), Quaternion.identity) as GameObject;
                    newDispenser.transform.localPosition = mySpot.transform.position;
                    spawnedDispenser = newDispenser;
                    currentImage.sprite = setItem.dispenserBuildIcon;
                    containedItem = setItem;
                }

                //hand is empty
            }
            else if (containedItem == null)
            {
                {
                    GameObject newDispenser = Instantiate(setItem.dispenser, new Vector2(mySpot.transform.position.x, mySpot.transform.position.y), Quaternion.identity) as GameObject;
                    newDispenser.transform.localPosition = mySpot.transform.position;
                    merchantManager.placed = 1;
                    spawnedDispenser = newDispenser;
                    containedItem = setItem;
                    currentImage.sprite = setItem.dispenserBuildIcon;
                    //merchantManager.buildItem = null;
                }
            }
        }
    }
}
