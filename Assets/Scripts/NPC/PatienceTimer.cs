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

    // Start is called before the first frame update
    void Start()
    {
        //Being(Duration);
        active = true;
    }

    public void StartTimer()
    {
        Being(Duration);
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
    }
}
