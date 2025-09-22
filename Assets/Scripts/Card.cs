using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] Image frontImage; //to assign in Inspector

    public int id; // identifier of the front image, to match pairs
    public GameObject front; // card front side
    public GameObject back; // card back side

    private bool isFaceUp;

    private GridManager gridManager; // to check match
    private GameManager gameManager;

    public void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    // Create the card
    public void SetCard(int imageId, Sprite imageSprite)
    {
        id = imageId;
        if (frontImage != null)
            frontImage.sprite = imageSprite;
    }
    
    // Flip the card to show either front or back (not front)
    public void Flip(bool showFront)
    {
        isFaceUp = showFront;
        front.SetActive(isFaceUp);
        back.SetActive(!isFaceUp);
    }

    // When the player click, if the card is face down, then flip the card. Otherwise nothing happened. 
    public void OnClick()
    {
        if (!isFaceUp && gameManager.gameActive)
        {
            gridManager.OnCardClicked(this);
        }
    }
}
