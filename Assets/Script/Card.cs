using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Card", order = 1)]
public class Card : ScriptableObject
{
    public enum CardTypes
    {
        Default = 0,
        WaxBean = 1,
        BlackEyedBean = 2,
        CocoaBean = 3,
        SoyBean = 4,
        ChiliBean = 5,
        RedBean = 6,
        BlueBean = 7,
        CoffeeBean = 8,
        StinkBean = 9,
        GreenBean = 10,
        GardenBean = 11,
    }

    [Serializable]
    public class StackCoinPair
    {
        public int stackCount;
        public int coinAmount;
    }

    [SerializeField]
    private CardTypes cardType;
    [SerializeField]
    private Sprite cardImage;
    [SerializeField]
    private string cardName;
    [SerializeField]
    private int amount;
    [SerializeField]
    private List<StackCoinPair> stackSizeToCoin = new List<StackCoinPair>();

    public CardTypes CardType { get { return cardType; } }
    public Sprite CardImage { get { return cardImage; } }
    public string Name { get { return cardName; } }
    public int Amount { get { return amount; } }
    public Dictionary<int, int> StackSizeToCoinMap { 
        get { 
            return stackSizeToCoin.ToDictionary(pair => pair.stackCount, pair => pair.coinAmount); 
        }
    }

}
