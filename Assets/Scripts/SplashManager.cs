using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashManager : MonoBehaviour
{
    public Image fadeImage;   // Assign in Inspector
    public float fadeDuration = 1f;
    public float displayTime = 2f; // How long logo stays visible

    private void Start()
    {
        StartCoroutine(PlaySplash());
    }

    private System.Collections.IEnumerator PlaySplash()
    {
        // Start fully black
        fadeImage.color = Color.black;

        // Fade in (reveal logo)
        yield return Fade(Color.black, Color.clear, fadeDuration);

        // Wait with logo visible
        yield return new WaitForSeconds(displayTime);

        // Fade out (to black)
        yield return Fade(Color.clear, Color.black, fadeDuration);

        // Load your main scene
        SceneManager.LoadScene("LoadingScene");
    }

    private System.Collections.IEnumerator Fade(Color from, Color to, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            fadeImage.color = Color.Lerp(from, to, t / duration);
            yield return null;
        }
    }
}
