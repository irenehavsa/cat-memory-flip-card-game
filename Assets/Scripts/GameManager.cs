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

    [SerializeField] int level = 1;
    //private int maxLevel = 7;

    //private int pairCount; // how many pairs to generate, can be different from total distict sprites
    public int remainingSteps = 5;
    private int remainingPairs;

    private int totalCoins = 100;

    public bool gameActive = false;

    public LevelLoader loader;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();

        StartLevel(level);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartLevel(int gameLevel)
    {
        int levelIndex = gameLevel - 1;
        LevelConfig config = loader.GetLevel(levelIndex);

        if (config == null) return;

        Debug.Log($"Starting Level {gameLevel} with {config.numberOfPairs} pairs");

        remainingSteps = config.availableSteps;

        //pairCount = level;
        gridManager.GenerateCards(config.numberOfPairs);
        remainingPairs = config.numberOfPairs;

        levelText.text = "Level " + config.level;
        remainingStepsText.text = "Steps " + remainingSteps;

        gameActive = true;
    }

    public void OnCardClicked(bool match)
    {
        Debug.Log("Masuk OnCardClicked in GameManager");
        remainingSteps--;
        remainingStepsText.text = "Steps " + remainingSteps;

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
        int coins = 10 + remainingSteps * 10;
        totalCoins += coins;

        winInfoText.text = "Remaining Steps: " + remainingSteps + "\nCoins: " + coins + "\nTotal Coins: " + totalCoins;

        winScreen.gameObject.SetActive(true);
        gameActive = false;
    }
    public void PlayerLose()
    {
        Debug.Log("masuk Player Lose");
        loseScreen.gameObject.SetActive(true);
        gameActive = false;
    }

    public void NextLevel()
    {
        if (level >= loader.levelConfigList.levels.Length)
        {
            Debug.Log("Message upcoming levels");
        } else
        {
            winScreen.gameObject.SetActive(false);

            level++;
            remainingSteps = 10;

            StartLevel(level);
        }
    }

    public void Restart()
    {
        loseScreen.gameObject.SetActive(false);
        settingsScreen.gameObject.SetActive(false);

        remainingSteps = 10;

        StartLevel(level);
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
}
