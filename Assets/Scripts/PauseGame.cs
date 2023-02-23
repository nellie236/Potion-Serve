using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public bool paused;
    public bool exit;
    public GameObject pauseScreen;
    public GameObject confirmExit;
    public GameObject returnButton;
    public GameObject exitButton;

    private void Start()
    {
        paused = false;
        exit = false;
        confirmExit.SetActive(false);
        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (!paused)
        {
            paused = true;
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            //pause everything, open all the pause tabs
        }
        else if (paused)
        {
            //unpause, close the pause tabs
            Time.timeScale = 1;
            paused = false;
            pauseScreen.SetActive(false);
        }
    }

    public void ExitButton()
    {
        if (!exit)
        {
            //turn on exit? are you sure?
            exit = true;
            returnButton.SetActive(false);
            exitButton.SetActive(false);
            confirmExit.SetActive(true);
        }
        else if (exit)
        {
            exit = false;
            confirmExit.SetActive(false);
            returnButton.SetActive(true);
            exitButton.SetActive(true);
        }
    }
}
