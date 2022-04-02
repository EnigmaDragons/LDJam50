using UnityEngine;

[CreateAssetMenu]
public class Plant : ScriptableObject
{
    [SerializeField] private float waterCapacity;
    [SerializeField] private float waterConsumptionPerSecond;
    [SerializeField] private float wiltingSeconds;

    public float WaterCapacity => waterCapacity;
    public float WaterConsumption => waterConsumptionPerSecond;
    public float WiltingSeconds => wiltingSeconds;
}
