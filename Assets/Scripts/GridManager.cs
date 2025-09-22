using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab; // card prefabs
    [SerializeField] Transform gridPanel; // the panel
    [SerializeField] Sprite[] cardSprites; // all available card sprites (front image)
    [SerializeField] int pairCount = 2; // how many pairs to generate, can be different from total distict sprites

    private List<GameObject> cards = new List<GameObject>(); // list of all cards created and placed in the grid

    private List<Card> flippedCards = new List<Card>(); // Use list (dynamic) for further levels when player have to match more than 2 cards

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateCards()
    {
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

    public void OnCardClicked(Card clickedCard)
    {
        if (flippedCards.Count >= 2) return; // block if 2 are already flipped

        clickedCard.Flip(true);
        flippedCards.Add(clickedCard);

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
        }
        else
        {
            Debug.Log("Not Match! Flip back the cards!");
            flippedCards[0].Flip(false);
            flippedCards[1].Flip(false);
        }

        flippedCards.Clear();
    }
}
