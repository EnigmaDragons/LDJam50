using UnityEngine;

[CreateAssetMenu]
public class Plant : ScriptableObject
{
    [SerializeField] private float waterCapacity;
    [SerializeField] private float waterConsumptionPerSecond;

    public float WaterCapacity => waterCapacity;
    public float WaterConsumption => waterConsumptionPerSecond;
}
