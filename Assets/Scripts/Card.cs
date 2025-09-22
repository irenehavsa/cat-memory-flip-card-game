using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] Image frontImage; //to assign in Inspector

    public int id; // identifier of the front image, to match pairs
    public GameObject front; // card front side
    public GameObject back; // card back side

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
        front.SetActive(showFront);
        back.SetActive(!showFront);
    }
}
