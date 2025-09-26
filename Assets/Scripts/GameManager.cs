using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GridManager gridManager;

    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI remainingStepsText;
    [SerializeField] TextMeshProUGUI heartsText;
    [SerializeField] TextMeshProUGUI coinsText;
    
    [SerializeField] GameObject winScreen;
    [SerializeField] TextMeshProUGUI winCoinsText;
    [SerializeField] TextMeshProUGUI winRemainingStepsText;
    [SerializeField] TextMeshProUGUI rewardsText;
    [SerializeField] Button nextButton;

    [SerializeField] GameObject loseScreen;
    [SerializeField] TextMeshProUGUI loseHeartsText;

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

        remainingSteps = config.steps;

        remainingPairs = config.pairs;
        gridManager.GenerateCards(config.pairs, config.col, config.row);

        levelText.text = "Level " + MainManager.instance.currentLevel;
        remainingStepsText.text = "" + remainingSteps;

        // Hearts
        heartsText.text = "∞"; //temporary
        /*if (MainManager.instance.hearts < 0)
        {
            Debug.Log("Infinity");
            heartsText.text = "∞";
        }
        else
        {
            Debug.Log("numbers");
            heartsText.text = "" + MainManager.instance.hearts;
        }*/

        coinsText.text = "" + MainManager.instance.coins;

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
        //Debug.Log("masuk Player Win");
        winCoinsText.text = "" + MainManager.instance.coins;
        winRemainingStepsText.text = "" + remainingSteps;

        int coinGained = remainingSteps * 10;
        MainManager.instance.coins += coinGained;
        rewardsText.text = "" + coinGained;

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
