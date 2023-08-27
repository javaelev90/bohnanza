using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using System.Linq;

public class Player : NetworkBehaviour
{
    public List<Card> Hand { get; private set; }
    public List<Card> Coins { get; private set; }
    public Field Field { get; private set; }
    public CardManager CardManager { get; set; }

    void Start()
    {
        Hand = new List<Card>();
        Coins = new List<Card>();
        Field = new Field();
    }

    [ClientRpc]
    public void ReceiveCardClientRpc(Card.CardTypes cardType)
    {
        Hand.Add(CardManager.CardTypes[cardType]);
    }
}
