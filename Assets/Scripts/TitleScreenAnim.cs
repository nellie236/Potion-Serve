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
    void Start()
    {
        Time.timeScale = 1;
        animationPlayed = false;
        MainCamera = GameObject.Find("Main Camera");
        Color start = text.color;
        start.a = 0.0f;
        text.color = start;
    }

    void Update()
    {
        if (animationPlayed && Input.anyKey)
        {
            TitleExitAnim();
            //MainCamera.GetComponent<LoadSceneTrigger>().LoadScene();
            StartCoroutine(FadeOut());
        }
    }

    public void IdleAnim()
    {
        animationPlayed = true;
        StartCoroutine(FadeIn());
    }

    public void TitleExitAnim()
    {
        myAnim.SetBool("Exit", true);
        animationPlayed = false;
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

    IEnumerator FadeOut()
    {
        targetAlpha = 0.0f;
        Color curColor = text.color;
        while (Mathf.Abs(curColor.a + targetAlpha) < 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, fadeRate * Time.deltaTime);
            text.color = curColor;
            yield return null;
        }
    }
    
}
