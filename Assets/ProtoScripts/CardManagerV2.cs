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
            if (!card.IsCardBeingDragged && !card.IsCardInteracting)
            {
                // TODO dont move card if being pressed or moved by player
                card.SetPosition(startPosition);
                card.SetRenderingOrderLevel(maxOrder);
            }
            else
            {
                card.SetRenderingOrderLevel(cards.Count + 1);
            }
            maxOrder--;
            startPosition += cardOffset;

        });
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
