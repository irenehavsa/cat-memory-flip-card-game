using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI heartsText;
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] TextMeshProUGUI currentLevelText;
    [SerializeField] TextMeshProUGUI nextLevelText1;
    [SerializeField] TextMeshProUGUI nextLevelText2;
    [SerializeField] TextMeshProUGUI nextLevelText3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateTexts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateTexts()
    {
        // Hearts
        Debug.Log("Number of hearts " + MainManager.instance.hearts);
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

        // Coins
        coinsText.text = "" + MainManager.instance.coins;

        // Level Orbs
        currentLevelText.text = "" + MainManager.instance.currentLevel;
        nextLevelText1.text = "" + (MainManager.instance.currentLevel + 1);
        nextLevelText2.text = "" + (MainManager.instance.currentLevel + 2);
        nextLevelText3.text = "" + (MainManager.instance.currentLevel + 3);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
