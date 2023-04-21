using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenAnim : MonoBehaviour
{
    public Animator myAnim;
    public GameObject MainCamera;
    public bool animationPlayed;


    private float targetAlpha;
    public float fadeRate;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        animationPlayed = false;
        MainCamera = GameObject.Find("Main Camera");
        Color start = text.color;
        start.a = 0.0f;
        text.color = start;
    }

    // Update is called once per frame
    void Update()
    {
        if (animationPlayed && Input.anyKey)
        {
            MainCamera.GetComponent<LoadSceneTrigger>().LoadScene();
        }
    }

    public void IdleAnim()
    {
        myAnim.SetBool("Idle", true);
        animationPlayed = true;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        targetAlpha = 1.0f;
        Color curColor = text.color;
        while (Mathf.Abs(curColor.a - targetAlpha) > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, fadeRate * Time.deltaTime);
            text.color = curColor;
            yield return null;
        }
        
    }
    
}
