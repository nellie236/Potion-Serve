using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDay : MonoBehaviour
{
    public GameObject keyPressImage;
    bool inTrigger;
    private void OnTriggerStay2D(Collider2D collision)
    {
        keyPressImage.SetActive(true);
        inTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        keyPressImage.SetActive(false);
        inTrigger = false;
    }

    private void Update()
    {
        if ((inTrigger == true) && (Input.GetKey(KeyCode.N)))
        {
            GameObject.Find("Main Camera").GetComponent<DayManager>().NextDay();
            GameObject.Find("Main Camera").GetComponent<LoadSceneTrigger>().LoadScene();
        }
    }
}
