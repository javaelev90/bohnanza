using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManagerV2 : MonoBehaviour
{
    [SerializeField]
    GameObject cardPrefab;

    [SerializeField]
    int cardSpacing = 0;

    [SerializeField]
    CardStackType cardStackType = CardStackType.NoStack;

    [SerializeField]
    Vector3 cardOffset = Vector3.zero;

    [SerializeField]
    CardMovementType cardMovementType = CardMovementType.Snap;

    [SerializeField]
    float cardMovementTime = 0.5f;

    List<CardV2> cards = new();
    Vector3 stackStartPosition = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        stackStartPosition = transform.position;
        EventSystem.Instance.RegisterListener<CardInteractionEvent>(OnCardInteractionEvent);
    }

    // Update is called once per frame
    void Update() { 
        Vector3 startPosition = stackStartPosition;
        int maxOrder = cards.Count;
        cards.ForEach(card =>
        {
            //if (!card.IsCardBeingDragged && !card.IsCardInteracting)
            //{
            //    // TODO dont move card if being pressed or moved by player
            //    card.SetPosition(startPosition);
            //    card.SetRenderingOrderLevel(maxOrder);
            //}
            //else
            //{
            //    card.SetRenderingOrderLevel(cards.Count + 1);
            //}
            UpdateCardPosition(card, startPosition, maxOrder);
            maxOrder--;
            startPosition += cardOffset;

        });
    }

    void UpdateCardPosition(CardV2 card, Vector2 startPosition, int layerOrder)
    {
        if (card.IsCardBeingDragged || card.IsCardInteracting)
        {
            card.SetRenderingOrderLevel(cards.Count + 1);
        }
        else if (!card.IsCardMoved && !card.IsAtLocation(startPosition))
        {
            if (cardMovementType == CardMovementType.Snap)
            {
                card.SetPosition(startPosition);
            }
            else if(cardMovementType == CardMovementType.Linear)
            {
                card.Transfer(startPosition, cardMovementTime);
            }
        }
        else
        {
            card.SetRenderingOrderLevel(layerOrder);
        }
    }

    public void AddCard(CardV2 card)
    {
        cards.Add(card);
    }
    public void RemoveCard(CardV2 card)
    {
        cards.Remove(card);
    }
    public void OnCardInteractionEvent(CardInteractionEvent cardInteractionEvent)
    {
        RemoveCard(cardInteractionEvent.Card);
    }
}

public enum CardStackType
{
    NoStack,
    RightStack,
    LeftStack
}

public enum CardMovementType
{
    Snap,
    Linear
}
