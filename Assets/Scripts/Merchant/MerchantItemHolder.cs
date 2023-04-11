using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantItemHolder : MonoBehaviour
{
    public MerchantItem myItem;
    public Sprite empty;

    public void clickItem()
    {
        GameObject.Find("Merchant").GetComponent<MerchantManager>().PassItem(myItem);
    }

    
}
