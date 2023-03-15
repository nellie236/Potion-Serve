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

    // Start is called before the first frame update
    void Start()
    {
        remainingOpen = openTime;
        merchantAnim = GetComponent<Animator>();
        merchantAnim.SetBool("shopActive", false);
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
        StartCoroutine(ShopTimeOpen());
    }

    public void closeStore()
    {
        //this one is called by timer of how long the shop will be open OnEnd() in enumerator 
        remainingOpen = openTime;
        merchantAnim.SetBool("shopActive", false);
        merchantAnim.SetBool("closeShop", true);
    }

    public void storeIdle()
    {
        merchantAnim.SetBool("shopInactive", true);
        StartCoroutine(WaitToOpen());
        //start corountine to choose random waiting time for opening back up. i think time open will always be the same, but between opens will differ
    }


}
