using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInterface : MonoBehaviour
{
    [SerializeField]
    private Image cardImage;
    
    public Card Card { get; private set; }

    public void Init(Card card)
    {
        Card = card;
        cardImage.sprite = card.CardImage;
    }
}
