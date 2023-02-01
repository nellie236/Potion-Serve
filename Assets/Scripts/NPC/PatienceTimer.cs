using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatienceTimer : MonoBehaviour
{
    [SerializeField] private Image uiFill;

    public int Duration;
    private int remainingDuration;
    private bool Pause;
    public bool active;

    public GameObject currentCustomer;

    // Start is called before the first frame update
    void Start()
    {
        //Being(Duration);
        active = true;
        uiFill.fillAmount = 0;
    }

    public void StartTimer()
    {
        Being(Duration);
        uiFill.fillAmount = 1;
        active = true;
    }
    
    private void Being(int Second)
    {
        remainingDuration = Second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            if (currentCustomer.GetComponent<CustomerAgent>().orderFulfilled)
            {
                ResetTimer();
                break;
            }
            if (!Pause)
            {
                uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
                remainingDuration--;
                yield return new WaitForSeconds(1f);
            }
        }
        OnEnd();
    }

    private void OnEnd()
    {
        //call end code here
        print("End");
        active = false;
        currentCustomer.GetComponent<CustomerAgent>().orderFulfilled = false;
        GameObject.Find("PatienceTimer").GetComponent<PatienceTimer>().ourCustomer(null);
        ResetTimer();
    }

    private void ResetTimer()
    {
        uiFill.fillAmount = 0;
    }

    public void ourCustomer(GameObject ourCustomer)
    {
        currentCustomer = ourCustomer;
    }
        
        
}
