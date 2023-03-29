using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantItemHolder : MonoBehaviour
{
    public MerchantItem myItem;

    public void clickItem()
    {
        GameObject.Find("Merchant").GetComponent<MerchantManager>().PassItem(myItem);
    }
}
