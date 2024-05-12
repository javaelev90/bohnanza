using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class PlayerActionHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerInterface playerInterface;

    [SerializeField]
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        if (!player.IsOwner)
            playerInterface.gameObject.SetActive(false);

        EventSystem.Instance.RegisterListener<LaneHarvestEvent>(OnLaneHarvestEvent);
        EventSystem.Instance.RegisterListener<LanePlantEvent>(OnLanePlantEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnLaneHarvestEvent(LaneHarvestEvent harvestEvent) 
    { 

    }

    private void OnLanePlantEvent(LanePlantEvent plantEvent)
    {
        //if (player.IsOwner)
            //player.PlantCardClientRpc();
        // put the card in the lane
        // remove a card
        // send update to all players
    }
}
