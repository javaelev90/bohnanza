using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Event
{
}

public class CardEvent : Event
{
    public Card.CardTypes CardType { get; set; }
}

public class LaneEvent : Event
{
    public Lane Lane { get; set; }
}

public class ReceiveCardEvent : CardEvent
{
}

public class RemoveCardEvent : CardEvent
{
}

public class LanePlantEvent : LaneEvent
{
}

public class LaneHarvestEvent : LaneEvent
{
}
