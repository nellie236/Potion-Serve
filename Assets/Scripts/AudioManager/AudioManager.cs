using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip defaultAmbience; 
    private AudioSource track01, track02;
    private bool isPlayingTrack01;
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        track01 = gameObject.AddComponent<AudioSource>();
        track02 = gameObject.AddComponent<AudioSource>();
        isPlayingTrack01 = true;

        SwapTrack(defaultAmbience);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwapTrack(AudioClip newClip)
    {
        track01.loop = true;
        track02.loop = true;
        StopAllCoroutines();
        StartCoroutine(FadeTrack(newClip));

        isPlayingTrack01 = !isPlayingTrack01;
    }

    public void ReturnToDefault()
    {
        SwapTrack(defaultAmbience);
    }

    private IEnumerator FadeTrack(AudioClip newClip)
    {
        float timeToFade = 2f;
        float timeElapsed = 0;

        if (isPlayingTrack01)
        {
            track02.clip = newClip;
            track02.Play();
            while (timeElapsed < timeToFade)
            {
                track02.volume = Mathf.Lerp(0, 0.25f, timeElapsed / timeToFade);
                track01.volume = Mathf.Lerp(0.25f, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            track01.Stop();
        }
        else
        {
            track01.clip = newClip;
            track01.Play();
            while (timeElapsed < timeToFade)
            {
                track01.volume = Mathf.Lerp(0, 0.25f, timeElapsed / timeToFade);
                track02.volume = Mathf.Lerp(0.25f, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            track02.Stop();
        }
    }
}
