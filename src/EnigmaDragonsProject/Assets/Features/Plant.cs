using UnityEngine;

public class Plant : ScriptableObject
{
    [SerializeField] private float waterCapacity;
    [SerializeField] private float waterConsumption;

    public float WaterCapacity => waterCapacity;
    public float WaterConsumption => waterConsumption;
}
