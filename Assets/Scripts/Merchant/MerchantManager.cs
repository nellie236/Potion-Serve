using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantManager : MonoBehaviour
{

    //bool shopActive;
    Animator merchantAnim;
    public float openTime;
    public float remainingOpen; //have to make sure that if the player HAS the shop open, time is paused
    public float remainingClose;
    
    public bool canAccessMerchant;
    public bool marketActive;

    public Camera mainCamera;
    public Canvas mainCanvas;
    public Camera marketCamera;
    public Canvas marketCanvas;
    public CharacterController2D Player;
    public CoinManager coinManager;

    public List<MerchantItem> sellables;
    public List<Button> forSale;
    public Text itemDescript;
    public Image displayIcon;
    public Text itemPrice;
    public Text playerCoins;
    public MerchantItem selectedItem;
    public MerchantItem buildItem;

    public GameObject buildMapParent;
    public bool building;
    public int placed; 

    // Start is called before the first frame update
    void Start()
    {
        placed = 0;
        remainingOpen = openTime;
        marketActive = false;
        merchantAnim = GetComponent<Animator>();
        merchantAnim.SetBool("shopActive", false);
        Player = GameObject.Find("Player").GetComponent<CharacterController2D>();
        coinManager = GameObject.Find("CoinManager").GetComponent<CoinManager>();
        //building = false;
        //buildMapParent.SetActive(false);
        displayIcon.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        playerCoins.text = "" + coinManager.coinCount;  
        
        if (displayIcon.sprite == null)
        {
            displayIcon.gameObject.SetActive(false);
        }
        else if (displayIcon.sprite != null)
        {
            displayIcon.gameObject.SetActive(true);
        }
    }

    private IEnumerator ShopTimeOpen()
    {
        {
            while (remainingOpen >= 0)
            {
                remainingOpen--;
                yield return new WaitForSeconds(1f);
            }
            CloseStore();
        }
    }

    private IEnumerator WaitToOpen()
    {
        float closedTime = Random.Range(10, 20);
        remainingClose = closedTime;
        while (remainingClose >= 0)
        {
            remainingClose--;
            yield return new WaitForSeconds(1f);
        }
        merchantAnim.SetBool("closeShop", false);
        merchantAnim.SetBool("shopInactive", false);
        yield return new WaitForSeconds(1f);
        OpenStore();
    }

    public void OpenStore()
    {
        merchantAnim.SetBool("shopActive", true);
        
        canAccessMerchant = true;
        StartCoroutine(ShopTimeOpen());
        ItemsToSell();
    }

    public void CloseStore()
    {
        //this one is called by timer of how long the shop will be open OnEnd() in enumerator 
        remainingOpen = openTime;
        merchantAnim.SetBool("shopActive", false);
        merchantAnim.SetBool("closeShop", true);
        
        canAccessMerchant = false;
    }

    public void StoreIdle()
    {
        merchantAnim.SetBool("shopInactive", true);
        StartCoroutine(WaitToOpen());
        //start corountine to choose random waiting time for opening back up. i think time open will always be the same, but between opens will differ
    }

    public void ToggleMarket()
    {
        marketActive = !marketActive;
        //Debug.Log(marketActive);

        if (!marketActive)
        {
            //switch camera back to regular, unpause time. 
            Time.timeScale = 1;
            mainCamera.gameObject.SetActive(true);
            mainCanvas.gameObject.SetActive(true);
            marketCamera.gameObject.SetActive(false);
            marketCanvas.gameObject.SetActive(false);
        }

        if (marketActive)
        {
            //switch camera to building camera, pause time so that it does not keep counting down to close
            Time.timeScale = 0;
            marketCamera.gameObject.SetActive(true);
            marketCanvas.gameObject.SetActive(true);
            mainCamera.gameObject.SetActive(false);
            mainCanvas.gameObject.SetActive(false);
            buildMapParent.SetActive(false);
            PassItem(null);
        }
        
    }

    public void ItemsToSell()
    {
        int amountOfItemsSold = 4;
        for (int i = 0; i < amountOfItemsSold; i++)
        {
            int randomSell = Random.Range(0, sellables.Count);
            forSale[i].GetComponent<MerchantItemHolder>().myItem = sellables[randomSell];
            forSale[i].image.sprite = sellables[randomSell].icon;
        }
    }

    public void PassItem(MerchantItem item)
    {
        if (item != null)
        {
            //pass item to the display one. display the item description and price. 
            itemDescript.text = item.itemDescription;
            displayIcon.sprite = item.icon;
            itemPrice.text = item.price + " Coins";
            selectedItem = item;
        }
        else if (item == null)
        {
            displayIcon.sprite = null;
            itemDescript.text = null;
            itemPrice.text = null;
            selectedItem = null;
        }

    }

    public void CheckItem()
    {
        if (selectedItem != null)
        {
            BuyItem(selectedItem);
        }
        //check the item in the buy slot.
        //then run BuyItem();
    }

    public void BuyItem(MerchantItem item)
    {
        if (coinManager.coinCount < item.price)
        {
            Debug.Log("not enough money!");
            return;
        }
        else if (coinManager.coinCount >= item.price)
        {
            coinManager.coinCount -= item.price; 

            if (item.inventoryItem)
            {
                InventoryManager invManager = GameObject.Find("InventoryManagerObject").GetComponent<InventoryManager>();
                if (invManager.inventoryFull)
                {
                    Debug.Log("inventory is full");
                    return;
                }
                else if (!invManager.inventoryFull)
                {
                    invManager.Add(item.item, 1);
                }
                
            }
            else if (item.recipePage)
            {
                RecipeBookManager recipeBookManager = GameObject.Find("RecipeBookManager").GetComponent<RecipeBookManager>();
                recipeBookManager.AddPage(item.page);
                sellables.Remove(item);

                foreach (Button button in forSale)
                {
                    if (button.GetComponent<MerchantItemHolder>().myItem == item)
                    {
                        button.GetComponent<MerchantItemHolder>().myItem = null;
                        button.GetComponent<Image>().sprite = button.GetComponent<MerchantItemHolder>().empty;
                    }
                }

                PassItem(null);
                //item.bought = true;
            }
            else if (item.itemDispenser)
            {
                //open build map pass down (item.dispenser)
                buildItem = selectedItem;
                placed = 0; 
                ToggleBuildMap();
            }
            else if (item.refreshStock)
            {
                ItemsToSell();
            }
        }
    }

    public void ToggleBuildMap()
    {
        building = !building;

        if (!building)
        {
            buildMapParent.SetActive(false);
        }
        
        if (building)
        {
            buildMapParent.SetActive(true);
        }
    }
}
