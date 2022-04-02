using System;
using UnityEngine;

[Serializable]
public class PlantState
{
    public int Id;
    public Transform Transform;
    public float Water;
    public float WiltingRemainingSeconds;
}