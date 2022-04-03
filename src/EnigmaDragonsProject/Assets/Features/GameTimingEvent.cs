using System;
using UnityEngine;

[Serializable]
public class GameTimingEvent
{
    [SerializeField] private float secondsToAppear;
    [SerializeField] private string description;
        
    public float SecondsToAppear => secondsToAppear;
    public string Description => description;
}