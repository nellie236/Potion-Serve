using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public Text coinDisplay;
    void Start()
    {
        coinDisplay.text = "" + coinCount;
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
    
}
