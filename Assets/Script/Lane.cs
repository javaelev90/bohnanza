using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane
{
    public List<Card> Content { get; private set; }

    public Lane()
    { 
        Content = new List<Card>();
    }
}
