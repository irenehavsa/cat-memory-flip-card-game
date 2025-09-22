using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GridManager gridManager;

    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI remainingStepsText;
    
    [SerializeField] GameObject winScreen;
    [SerializeField] TextMeshProUGUI winInfoText;
    [SerializeField] Button continueButton;

    [SerializeField] GameObject loseScreen;

    [SerializeField] int level = 1;
    private int maxLevel = 7;

    private int pairCount; // how many pairs to generate, can be different from total distict sprites
    public int remainingSteps = 5;
    private int remainingPairs;

    private int totalCoins = 100;

    public bool gameActive = false;

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
        levelText.text = "Level " + level;
        remainingStepsText.text = "Steps " + remainingSteps;

        pairCount = level;
        gridManager.GenerateCards(pairCount);
        remainingPairs = pairCount;

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
        if (level >= maxLevel)
        {
            Debug.Log("Message upcoming levels");
        } else
        {
            winScreen.gameObject.SetActive(false);

            level++;
            remainingSteps = 10;

            StartLevel();
        }
    }

    public void Restart()
    {
        loseScreen.gameObject.SetActive(false);

        remainingSteps = 10;

        StartLevel();
    }
}
