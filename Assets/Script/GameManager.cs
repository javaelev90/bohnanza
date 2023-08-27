using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{

    [SerializeField]
    private CardManager cardManager;

    private IReadOnlyList<ulong> playerIds = new List<ulong>();

    public void Init()
    {
        cardManager.Init();
    }

    public void StartGame()
    {
        if (!IsHost) return;
        
        playerIds = NetworkManager.Singleton.ConnectedClientsIds;
        cardManager.DealCardsServerRpc(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
