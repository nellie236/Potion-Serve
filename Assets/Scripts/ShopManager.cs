using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject onLever;
    public GameObject offLever;
    bool inTrigger;

    public GameObject openSign;
    public GameObject openLight;
    public GameObject closedSign;
    bool hitLever;

    private void Start()
    {
        hitLever = false;
        openSign.SetActive(true);
        openLight.SetActive(true);
        onLever.SetActive(true);
        offLever.SetActive(false);
        closedSign.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTrigger = false;
        }
    }

    private void Update()
    {
        if ((inTrigger == true) && (Input.GetKeyUp(KeyCode.N)))
        {
            hitLever = !hitLever;
            //GameObject.Find("Main Camera").GetComponent<DayManager>().NextDay();
            //GameObject.Find("Main Camera").GetComponent<LoadSceneTrigger>().LoadScene();
            SwitchOpenClose();
        }

    }

    public void SwitchOpenClose()
    {
        if (hitLever)
        {
            onLever.SetActive(false);
            openLight.SetActive(false);
            closedSign.SetActive(true);
            offLever.SetActive(true);
        }

        if (!hitLever)
        {
            onLever.SetActive(true);
            openLight.SetActive(true);
            closedSign.SetActive(false);
            offLever.SetActive(false);
        }
    }
}
