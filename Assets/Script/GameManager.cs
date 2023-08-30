using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : SingletonNetworkBehaviour<GameManager>
{
    [SerializeField]
    private CardManager cardManager;
    [SerializeField]
    private SeatHandler seatHandler;

    public List<Player> PlayerList { get; private set; }

    public void StartGame()
    {
        if (!IsHost) return;

        PlayerList = new List<Player>();
        PlayerList.AddRange(GameObject.FindObjectsByType<Player>(FindObjectsSortMode.None));

        seatHandler.AssignPlayerSeatsServerRpc();

        cardManager.Init();
        cardManager.DealCardsServerRpc(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
