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
    Animator myAnim;

    public GameObject currentCustomer;

    // Start is called before the first frame update
    void Start()
    {
        //Being(Duration);
        //active = true;
        uiFill.fillAmount = 0;
        myAnim = this.gameObject.GetComponent<Animator>();
    }

    public void StartTimer()
    {
        Being(Duration);
        uiFill.fillAmount = 1;
        active = true;
        TimerEnter();
    }

    public void ContinueTimer()
    {
        StartCoroutine(UpdateTimer());
        TimerEnter();
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
        //print("End");
        active = false;
        currentCustomer.GetComponent<CustomerAgent>().orderFulfilled = false;
        GameObject.Find("PatienceTimer").GetComponent<PatienceTimer>().ourCustomer(null);
        ResetTimer();
        TimerExit();
        GameObject.Find("AudioManager").GetComponent<AudioManager>().ReturnToDefault();
    }

    private void ResetTimer()
    {
        uiFill.fillAmount = 0;
    }

    public void ourCustomer(GameObject ourCustomer)
    {
        currentCustomer = ourCustomer;
    }

    public void TimerEnter()
    {
        myAnim.SetBool("away", false);
        myAnim.SetBool("exit", false);
        myAnim.SetBool("enter", true);
    }

    public void TimerIdle()
    {
        myAnim.SetBool("idle", true);
        myAnim.SetBool("enter", false);
    }


    public void TimerExit()
    {
        myAnim.SetBool("exit", true);
        myAnim.SetBool("idle", false);
    }

    public void TimerHide()
    {
        myAnim.SetBool("away", true);
        myAnim.SetBool("exit", false);
    }
        
        
}
