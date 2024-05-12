using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInterface : MonoBehaviour
{
    [SerializeField]
    private TMP_Text handText;
    [SerializeField]
    private CardInterface cardPrefab;
    [SerializeField]
    private Transform handTransform;
    [SerializeField]
    private FieldInterface fieldInterface;

    private List<CardInterface> hand = new List<CardInterface>();

    private void Start()
    {
        EventSystem.Instance.RegisterListener<ReceiveCardEvent>(OnReceiveCardEvent);
        EventSystem.Instance.RegisterListener<RemoveCardEvent>(OnRemoveCardEvent);
    }

    public void OnReceiveCardEvent(ReceiveCardEvent receiveCardEvent)
    {
        CardInterface newCard = Instantiate(cardPrefab, handTransform);
        hand.Add(newCard);
        newCard.Init(CardManager.Instance.CardTypes[receiveCardEvent.CardType]);
    }

    public void OnRemoveCardEvent(RemoveCardEvent removeCardEvent)
    {
        hand.RemoveAt(0);
    }
}
