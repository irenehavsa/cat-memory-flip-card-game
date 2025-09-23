using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] GameObject cardPrefab; // card prefabs
    
    [SerializeField] Transform gridPanel; // the panel
    [SerializeField] GridLayoutGroup gridLayoutGroup;
    [SerializeField] RectTransform gridRectTransform;


    [SerializeField] Sprite[] cardSprites; // all available card sprites (front image)
    
    private List<GameObject> cards = new List<GameObject>(); // list of all cards created and placed in the grid

    private List<Card> flippedCards = new List<Card>(); // Use list (dynamic) for further levels when player have to match more than 2 cards

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateCards(int pairCount, int maxCol)
    {
        ClearGrid(); // make sure the grid is cleared before adding new cards

        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount; // Make sure constraint is set
        gridLayoutGroup.constraintCount = maxCol;

        AdjustCellSize(maxCol);

        // list of card ids being played
        List<int> ids = new List<int>(); // generate id for each card pair

        // create pairs: [0,0,1,1,2,2,...]
        for (int i = 0; i < pairCount; i++)
        {
            ids.Add(i);
            ids.Add(i);
        }

        // shuffle the ids 
        for (int i = 0; i < ids.Count; i++)
        {
            int rand = Random.Range(0, ids.Count);
            (ids[i], ids[rand]) = (ids[rand], ids[i]);
        }    

        // instatitate the cards
        foreach (int id in ids)
        {
            GameObject newCard = Instantiate(cardPrefab, gridPanel); // crate a card, put in the grid

            // access the Card script
            Card card = newCard.GetComponent<Card>(); 
            card.SetCard(id, cardSprites[id]);
            card.Flip(false); // start face down
            
            cards.Add(newCard);

            Debug.Log("Card id: " + id + " created, isFaceDown");
        }
    }

    private void ClearGrid()
    {
        foreach (Transform child in gridPanel)
        {
            Destroy(child.gameObject);
        }
    }

    public void OnCardClicked(Card clickedCard)
    {
        if (flippedCards.Count >= 2) return; // block if 2 are already flipped

        clickedCard.Flip(true);
        flippedCards.Add(clickedCard);

        gameManager.ReduceSteps(1);

        if (flippedCards.Count == 2)
        {
            StartCoroutine(CheckMatch());
        }
    }

    private System.Collections.IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.7f); // wait a moment so the player can see both cards

        if (flippedCards[0].id == flippedCards[1].id)
        {
            Debug.Log("Match!");
            gameManager.OnCardClicked(true); // tell the game manager it match
        }
        else
        {
            Debug.Log("Not Match! Flip back the cards!");
            flippedCards[0].Flip(false);
            flippedCards[1].Flip(false);
            gameManager.OnCardClicked(false); // tell the game manager it does not match
        }

        flippedCards.Clear();
    }

    private void AdjustCellSize(int cols)
    {
        // Get total width and height of the panel
        float panelWidth = gridRectTransform.rect.width;
        float panelHeight = gridRectTransform.rect.height;

        // Substract spacing
        float spacingX = gridLayoutGroup.spacing.x * (cols - 1);
        float cellWidth = (panelWidth - spacingX - gridLayoutGroup.padding.left - gridLayoutGroup.padding.right) / cols;

        gridLayoutGroup.cellSize = new Vector2(cellWidth, cellWidth);
    }
}
