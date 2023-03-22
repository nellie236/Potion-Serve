using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        remainingOpen = openTime;
        marketActive = false;
        merchantAnim = GetComponent<Animator>();
        merchantAnim.SetBool("shopActive", false);
        Player = GameObject.Find("Player").GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ShopTimeOpen()
    {
        {
            while (remainingOpen >= 0)
            {
                remainingOpen--;
                yield return new WaitForSeconds(1f);
            }
            closeStore();
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
        openStore();
    }

    public void openStore()
    {
        merchantAnim.SetBool("shopActive", true);
        
        canAccessMerchant = true;
        StartCoroutine(ShopTimeOpen());
    }

    public void closeStore()
    {
        //this one is called by timer of how long the shop will be open OnEnd() in enumerator 
        remainingOpen = openTime;
        merchantAnim.SetBool("shopActive", false);
        merchantAnim.SetBool("closeShop", true);
        
        canAccessMerchant = false;
    }

    public void storeIdle()
    {
        merchantAnim.SetBool("shopInactive", true);
        StartCoroutine(WaitToOpen());
        //start corountine to choose random waiting time for opening back up. i think time open will always be the same, but between opens will differ
    }

    public void ToggleMarket()
    {
        marketActive = !marketActive;

        if (marketActive)
        {
            //switch camera to building camera, pause time so that it does not keep counting down to close
            Time.timeScale = 0;
            marketCamera.enabled = true;
            mainCamera.enabled = false;
        }

        if (!marketActive)
        {
            //switch camera back to regular, unpause time. 
            Time.timeScale = 1;
            marketCamera.enabled = false;
            mainCamera.enabled = true;
        }
    }


}
