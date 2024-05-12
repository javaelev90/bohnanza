using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class Player : NetworkBehaviour
{
    NetworkTransform playerTransform;

    public List<Card> Hand { get; private set; }
    public List<Card> Coins { get; private set; }
    public Field Field { get; private set; }

    void Start()
    {
        Hand = new List<Card>();
        Coins = new List<Card>();
        Field = new Field();
    }

    [ClientRpc]
    public void ReceiveCardClientRpc(Card.CardTypes cardType)
    {
        if(CardManager.Instance.CardTypes.ContainsKey(cardType))
            Hand.Add(CardManager.Instance.CardTypes[cardType]);
        
        if (IsOwner)
            EventSystem.Instance.FireEvent(new ReceiveCardEvent { CardType = cardType });
    }

    [ClientRpc]
    public void ReceiveSeatPositionClientRpc(int seatId)
    {
        Debug.Log("got new position: " + seatId+" pos: "+ SeatHandler.Instance.GetSeat(seatId).position);
        transform.SetPositionAndRotation(
            SeatHandler.Instance.GetSeat(seatId).position,
            SeatHandler.Instance.GetSeat(seatId).rotation);
    }

    [ClientRpc]
    public void PlantCardClientRpc(Card.CardTypes cardType)
    {
        if (IsOwner)
        {
            Hand.RemoveAt(0);
            EventSystem.Instance.FireEvent(new RemoveCardEvent { CardType = cardType });
        }
            
    }
}
