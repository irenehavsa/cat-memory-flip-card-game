using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public Slider loadingBar;   // Assign in Inspector
    public string sceneToLoad;  // Set the next scene’s name in Inspector

    void Start()
    {
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        float startTime = Time.time;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // Progress goes from 0 → 0.9 while loading
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;

            // Once it’s fully loaded (reaches 0.9), activate the scene
            if (operation.progress >= 0.9f)
            {
                // Ensure at least 1 second passed
                float elapsed = Time.time - startTime;
                if (elapsed < 1f)
                {
                    yield return new WaitForSeconds(1f - elapsed);
                }

                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
