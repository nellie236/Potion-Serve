using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantItem : ScriptableObject
{
    public int price;
    public Sprite icon;

    public bool inventoryItem;
    public ItemClass item;
    public bool recipePage;
    public GameObject page;
    public bool itemDispenser;
    public GameObject dispenser;

}