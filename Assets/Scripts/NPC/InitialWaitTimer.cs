using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialWaitTimer : MonoBehaviour
{
    public int Duration;
    private int remainingDuration;
    public bool hasPatience;
    
    public GameObject currentCustomer;

    // Start is called before the first frame update
    void Start()
    {
        hasPatience = true;
    }

    public void StartWaitTimer()
    {
        Being(Duration);
    }

    private void Being(int Second)
    {
        remainingDuration = Second;
        StartCoroutine(UpdateWaitTimer());
    }

    private IEnumerator UpdateWaitTimer()
    {
        while (remainingDuration >= 0)
        {
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }
        OnEnd();
    }

    private void OnEnd()
    {
        hasPatience = false;
    }

    public void ourCustomer(GameObject ourCustomer)
    {
        currentCustomer = ourCustomer;
    }
}
