using System;
using UnityEngine;

[Serializable]
public class GameTimingEvent
{
    [SerializeField] private GameObject tile;
    [SerializeField] private float secondsToAppear;

    public GameObject Tile => tile;
    public float SecondsToAppear => secondsToAppear;
}