using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class CardManager : SingletonNetworkBehaviour<CardManager>
{
    [SerializeField]
    private List<Card> cards;

    public Dictionary<Card.CardTypes, Card> CardTypes = new();
    private Dictionary<Card.CardTypes, Dictionary<int, int>> stackSizeToCoinDict = new();
    private Queue<Card> deck = new();
    private List<Card> discard = new();
    private bool initiated = false;

    void Start()
    {
        cards.ForEach(card => CardTypes.Add(card.CardType, card));
    }

    public void Init()
    {
        if (!IsHost) return;
        if (initiated) return;
        
        // Add cards to dictionary
        cards.ForEach(
            card => {
                // Add cards to deck based on card amount
                Enumerable.Range(0, card.Amount).ToList().ForEach(
                    _ => deck.Enqueue(Instantiate(card)));
                // Store mapping in dictionary
                stackSizeToCoinDict.Add(card.CardType, card.StackSizeToCoinMap);
            });
        deck = Shuffle(deck);
        initiated = true;
        
    }

    [ServerRpc]
    public void DealCardsServerRpc(int numberOfCards)
    {
        if (!IsHost) return;

        IReadOnlyList<ulong> playerIds = NetworkManager.Singleton.ConnectedClientsIds;
        while (numberOfCards > 0)
        {
            foreach (Player player in GameManager.Instance.PlayerList)
            {
                player.ReceiveCardClientRpc(deck.Dequeue().CardType);
            }
            numberOfCards--;
        }

    }

    public Queue<Card> Shuffle(IEnumerable<Card> collection) 
    {
        // Ordering by giving a random order number to each item
        System.Random rand = new();
        return new Queue<Card>(collection.OrderBy(_ => rand.Next()).ToArray());
    }

}
