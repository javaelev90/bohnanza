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

    private void Start()
    {
        EventSystem.Instance.RegisterListener<ReceiveCardEvent>(OnReceiveCardEvent);
    }

    public void OnReceiveCardEvent(ReceiveCardEvent receiveCardEvent)
    {
        CardInterface newCard = Instantiate(cardPrefab, handTransform);
        newCard.Init(CardManager.Instance.CardTypes[receiveCardEvent.CardType]);
    }
}
