using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIAction : MonoBehaviour
{
    [SerializeField]
    CardManagerV2 cardManager;
    [SerializeField]
    GameObject cardPrefab;

    public void OnClickAddCard()
    {
        GameObject cardGameObject = Instantiate(cardPrefab);
        CardV2 cardComponent = cardGameObject.GetComponent<CardV2>();
        //cardComponent.Initialize(card);
        cardManager.AddCard(cardComponent);
    }
}
