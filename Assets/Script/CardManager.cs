using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class CardManager : NetworkBehaviour
{
    [SerializeField]
    private List<Card> cards;

    public Dictionary<Card.CardTypes, Card> CardTypes = new();
    private Dictionary<Card.CardTypes, Dictionary<int, int>> stackSizeToCoinDict = new();
    private Queue<Card> deck = new();
    private List<Card> discard = new();
    
    public List<Player> PlayerList { get; private set; }

    void Start()
    {
        PlayerList = new List<Player>();
        cards.ForEach(card => CardTypes.Add(card.CardType, card));
    }

    public void Init()
    {
        PlayerList.AddRange(GameObject.FindObjectsByType<Player>(FindObjectsSortMode.None));
        PlayerList.ForEach(player => player.CardManager = this);

        if (IsHost)
        {
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
        }
    }

    [ServerRpc]
    public void DealCardsServerRpc(int numberOfCards)
    {
        if (!IsHost) return;
        IReadOnlyList<ulong> playerIds = NetworkManager.Singleton.ConnectedClientsIds;
        while (numberOfCards > 0)
        {
            //foreach (ulong id in playerIds)
            //{

            //}
            foreach (Player player in PlayerList)
            {
                player.ReceiveCardClientRpc(deck.Dequeue().CardType);
            }
            numberOfCards--;
        }

    }

    public Queue<Card> Shuffle(IEnumerable<Card> collection) 
    {
        System.Random rand = new();
        return new Queue<Card>(collection.OrderBy(_ => rand.Next()).ToArray());
    }

}
