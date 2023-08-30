using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Event
{
}

public class ReceiveCardEvent : Event
{
    public Card.CardTypes CardType { get; set; }
}