using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneAsyc : MonoBehaviour
{
    [SerializeField] Slider progressBar;

    private void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(LoadingData.sceneToLoad);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            progressBar.value = operation.progress;

            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
