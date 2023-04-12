using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTips : MonoBehaviour
{
    public string currentKey;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.tag == "ToolTipsCustomer")
        {
            if (player.GetComponent<CharacterController2D>().currentCustomer != null)
            {
                if (player.GetComponent<CharacterController2D>().currentCustomer.GetComponent<CustomerAgent>().inProgState == CustomerStateId.OrderInProgress)
                {
                    currentKey = "Q";
                }
                else
                {
                    currentKey = "R";
                }
            }
        }
    }


}
