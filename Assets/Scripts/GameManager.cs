using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GridManager gridManager;

    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI remainingStepsText;
    
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject allCompletedScreen;
    [SerializeField] GameObject loseScreen;

    //public LevelLoader loader;

    private int remainingSteps;
    private int remainingPairs;
    public bool gameActive = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gridManager = FindFirstObjectByType<GridManager>();

        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartLevel()
    {
        Debug.Log("MainManager current level: " + MainManager.instance.currentLevel + "\nLevels length: " + LevelLoader.Instance.levelConfigList.levels.Length);
        if (MainManager.instance.currentLevel >= LevelLoader.Instance.levelConfigList.levels.Length)
        {
            Debug.Log("Message upcoming levels");
        }

        //TO DO Fix logic
        int realLevel = Mathf.Min(MainManager.instance.currentLevel, LevelLoader.Instance.levelConfigList.levels.Length);

        int levelIndex = realLevel - 1;
        LevelConfig config = LevelLoader.Instance.GetLevel(levelIndex);

        if (config == null) return;

        remainingSteps = config.steps;
            
        remainingPairs = config.pairs;
        gridManager.GenerateCards(config.pairs, config.col, config.row);

        levelText.text = "Level " + realLevel;
        remainingStepsText.text = "" + remainingSteps;

        gameActive = true;
    }

    public void OnCardClicked(bool match)
    {
        if (match)
        {
            remainingPairs--;
        }

        if (remainingPairs <= 0)
        {
            PlayerWin();
        }
        else if (remainingSteps <= 0)
        {
            PlayerLose();
        }
    }

    public void PlayerWin()
    {
        gameActive = false;

        // Current level masih bisa lebih 1 dari max level
        if (MainManager.instance.currentLevel <= LevelLoader.Instance.levelConfigList.levels.Length)
        {
            MainManager.instance.currentLevel++;
        }

        if (MainManager.instance.currentLevel > LevelLoader.Instance.levelConfigList.levels.Length)
        {
            allCompletedScreen.gameObject.SetActive(true);
        }
        else {
            winScreen.gameObject.SetActive(true);
        }

        MainManager.instance.SaveData();
    }
    public void PlayerLose()
    {
        loseScreen.gameObject.SetActive(true);
        gameActive = false;
    }

    public void OnNextButtonClicked()
    {
        winScreen.gameObject.SetActive(false);
        StartLevel();
    }
    public void OnPlayAgainButtonClicked()
    {
        allCompletedScreen.gameObject.SetActive(false);
        StartLevel();
    }

    public void OnRetryButtonClicked()
    {
        loseScreen.gameObject.SetActive(false);
        StartLevel();
    }

    public void OnHomeButtonClicked()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void ReduceSteps(int steps)
    {
        remainingSteps -= steps;
        remainingStepsText.text = "" + remainingSteps;
    }
}
