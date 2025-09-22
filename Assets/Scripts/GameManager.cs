using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    private GridManager gridManager;

    [SerializeField] TextMeshProUGUI levelText;

    [SerializeField] int level = 1;
    private int pairCount; // how many pairs to generate, can be different from total distict sprites


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();

        levelText.text = "Level " + level;

        pairCount = level;
        gridManager.GenerateCards(pairCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
