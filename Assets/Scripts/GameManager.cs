using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GridManager gridManager;

    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI remainingStepsText;
    
    [SerializeField] GameObject winScreen;
    [SerializeField] TextMeshProUGUI winInfoText;
    [SerializeField] Button continueButton;

    [SerializeField] GameObject loseScreen;
    [SerializeField] GameObject settingsScreen;

    //private int level = 1;
    //private int maxLevel = 7;

    //private int pairCount; // how many pairs to generate, can be different from total distict sprites
    private int remainingSteps;
    private int remainingPairs;

    //private int totalCoins = 0;

    public bool gameActive = false;

    public LevelLoader loader;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();

        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartLevel()
    {
        if (MainManager.instance.currentLevel >= loader.levelConfigList.levels.Length)
        {
            Debug.Log("Message upcoming levels");
        }

        int levelIndex = MainManager.instance.currentLevel - 1;
        LevelConfig config = loader.GetLevel(levelIndex);

        if (config == null) return;

        //Debug.Log($"Starting Level {gameLevel} with {config.numberOfPairs} pairs");

        remainingSteps = config.availableSteps;

        //pairCount = level;
        gridManager.GenerateCards(config.numberOfPairs, config.col);
        remainingPairs = config.numberOfPairs;

        levelText.text = "Level " + config.level;
        remainingStepsText.text = "" + remainingSteps;

        gameActive = true;
    }

    public void OnCardClicked(bool match)
    {
        Debug.Log("Masuk OnCardClicked in GameManager");

        if (match)
        {
            remainingPairs--;
        }
        Debug.Log("Remaining Steps: " + remainingSteps + " - Remaining Pairs: " + remainingPairs);

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
        Debug.Log("masuk Player Win");
        int coinGained = remainingSteps * 10;
        MainManager.instance.coins += coinGained;

        winInfoText.text = "Remaining Steps: " + remainingSteps + "\nCoins: " + coinGained + "\nTotal Coins: " + MainManager.instance.coins;

        winScreen.gameObject.SetActive(true);
        gameActive = false;

        MainManager.instance.currentLevel++;
        MainManager.instance.SaveData();
    }
    public void PlayerLose()
    {
        Debug.Log("masuk Player Lose");
        loseScreen.gameObject.SetActive(true);
        gameActive = false;
    }

    public void NextLevel()
    {
        winScreen.gameObject.SetActive(false);
        StartLevel();
    }

    public void Restart()
    {
        loseScreen.gameObject.SetActive(false);
        settingsScreen.gameObject.SetActive(false);

        remainingSteps = 10;

        StartLevel();
    }

    public void OpenHomeScreen()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void OpenSettings()
    {
        //Debug.Log("masuk Player Lose");
        settingsScreen.gameObject.SetActive(true);
        gameActive = false;
    }
    public void CloseSettings()
    {
        //Debug.Log("masuk Player Lose");
        settingsScreen.gameObject.SetActive(false);
        gameActive = true;
    }

    public void ReduceSteps(int steps)
    {
        remainingSteps -= steps;
        remainingStepsText.text = "" + remainingSteps;
    }
}
