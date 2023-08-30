using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SeatHandler : SingletonNetworkBehaviour<SeatHandler>
{
    [SerializeField]
    private List<Transform> seatPositions;

    public Dictionary<int, Transform> SeatDictionary {  get; private set; }
    //public Dictionary<Player, int> PlayerSeat { get; private set; }


    private void Start()
    {
        SeatDictionary = new Dictionary<int, Transform>();
        for (int i = 0; i < seatPositions.Count; i++)
        {
            SeatDictionary.Add(i, seatPositions[i]);
        }
    }

    public Transform GetSeat(int id)
    {
        return SeatDictionary[id];
    }

    [ServerRpc]
    public void AssignPlayerSeatsServerRpc()
    {
        if (IsHost)
        {
            List<Player> playerList = GameManager.Instance.PlayerList;
            for (int i = 0; i < playerList.Count && i < seatPositions.Count; i++)
            {
                Player player = playerList[i];
                player.ReceiveSeatPositionClientRpc(i);
            }
        }

    }
}
