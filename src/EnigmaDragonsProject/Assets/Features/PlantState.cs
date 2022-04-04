using System;
using UnityEngine;

[Serializable]
public class PlantState
{
    public string Name;
    public int Id;
    public Transform Transform;
    public float Water;
    public float WiltingRemainingSeconds;
    public float WaterCapacity;
    public bool IsOnFire;
}