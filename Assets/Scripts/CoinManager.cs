using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public Text coinDisplay;
    int frostRecipeCost;
    int healthRecipeCost;
    public GameObject unlockFrost;
    public GameObject frostButton;
    public GameObject frostDispenser;
    public GameObject frostCustomer;
    public GameObject difficultFrostCustomer;
    public GameObject unlockHealth;
    public GameObject healthButton;
    public GameObject healthDispenser1;
    public GameObject healthDispenser2;
    public GameObject healthCustomer;
    private void Awake()
    {
        frostDispenser.SetActive(false);
        healthDispenser1.SetActive(false);
        healthDispenser2.SetActive(false);
    }
    void Start()
    {
        coinDisplay.text = "" + coinCount;
        frostRecipeCost = 15;
        healthRecipeCost = 45;
        unlockFrost.SetActive(false);
        unlockHealth.SetActive(false);
    }

    private void Update()
    {
        coinDisplay.text = "" + coinCount;
    }

    public void AddCoins(int coinAmount)
    {
        coinCount = coinCount + coinAmount;
    }

    public void RemoveCoins(int coinAmount)
    {
        coinCount = coinCount - coinAmount;
    }

    public void UnlockFrostRecipe()
    {
        if (coinCount >= frostRecipeCost)
        {
            unlockFrost.SetActive(true);
            frostDispenser.SetActive(true);
            GameObject.Find("CustomerManager").GetComponent<CustomerManager>().AddCustomer(frostCustomer);
            GameObject.Find("CustomerManager").GetComponent<CustomerManager>().AddCustomer(difficultFrostCustomer);
            RemoveCoins(frostRecipeCost);
            Destroy(frostButton);
        }
    }

    public void UnlockHealthRecipe()
    {
        if (coinCount >= healthRecipeCost)
        {
            unlockHealth.SetActive(true);
            healthDispenser1.SetActive(true);
            healthDispenser2.SetActive(true);
            GameObject.Find("CustomerManager").GetComponent<CustomerManager>().AddCustomer(healthCustomer);
            RemoveCoins(healthRecipeCost);
            Destroy(healthButton);
        }
    }
    
}
