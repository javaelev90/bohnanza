using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field
{
    public enum Level
    {
        Default,
        LevelOne,
        LevelTwo,
    }

    public List<Lane> FieldLanes { get; private set; }
    public Level FieldLevel { get; private set; }

    void Start()
    {
        FieldLanes = new List<Lane>()
        {
            new(), new()
        };

        FieldLevel = Level.LevelOne;
    }

    void Update()
    {
        
    }

    void OnLaneHarvestEvent(LaneHarvestEvent laneHarvestEvent)
    {
        
    }
}
