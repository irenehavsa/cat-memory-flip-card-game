using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab; // card prefabs
    [SerializeField] Transform gridPanel; // the panel
    [SerializeField] Sprite[] cardSprites; // all available card sprites (front image)
    [SerializeField] int pairCount = 2; // how many pairs to generate, can be different from total distict sprites

    private List<GameObject> cards = new List<GameObject>(); // list of all cards created and placed in the grid

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
            card.Flip(true); // start face down
            
            cards.Add(newCard);
        }
    }
}
