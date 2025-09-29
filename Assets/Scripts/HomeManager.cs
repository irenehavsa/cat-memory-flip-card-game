using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentLevelText;
    [SerializeField] TextMeshProUGUI nextLevelText1;
    [SerializeField] TextMeshProUGUI nextLevelText2;
    [SerializeField] GameObject nextLevel1;
    [SerializeField] GameObject nextLevel2;

    [SerializeField] GameObject allCompleted;

    [SerializeField] TextMeshProUGUI playText;

    //public LevelLoader loader;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateLevelList();
        ShowAllComplete();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateLevelList()
    {
        // Levels
        currentLevelText.text = "" + Mathf.Min(MainManager.instance.currentLevel, LevelLoader.Instance.levelConfigList.levels.Length);

        if ((MainManager.instance.currentLevel + 1) <= LevelLoader.Instance.levelConfigList.levels.Length)
        {
            nextLevel1.gameObject.SetActive(true);
            nextLevelText1.text = "" + (MainManager.instance.currentLevel + 1);
        }

        if ((MainManager.instance.currentLevel + 2) <= LevelLoader.Instance.levelConfigList.levels.Length)
        {
            nextLevel2.gameObject.SetActive(true);
            nextLevelText2.text = "" + (MainManager.instance.currentLevel + 2);
        }
    }

    private void ShowAllComplete()
    {
        if (MainManager.instance.currentLevel > LevelLoader.Instance.levelConfigList.levels.Length)
        {
            playText.text = "Play Again";
            allCompleted.gameObject.SetActive(true);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
