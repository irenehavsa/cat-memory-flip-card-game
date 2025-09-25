using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public Image loadingFill;       // Assign ProgressBarFill in Inspector
    public string sceneToLoad = "HomeScene";      // Set next scene in Inspector
    public float minDisplayTime = 1.5f;

    private float currentProgress = 0f;

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
            float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);

            // Smooth bar movement
            currentProgress = Mathf.MoveTowards(currentProgress, targetProgress, Time.deltaTime);
            loadingFill.fillAmount = currentProgress; // <--- key difference

            if (operation.progress >= 0.9f && currentProgress >= 1f)
            {
                float elapsed = Time.time - startTime;
                if (elapsed < minDisplayTime)
                {
                    yield return new WaitForSeconds(minDisplayTime - elapsed);
                }

                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
