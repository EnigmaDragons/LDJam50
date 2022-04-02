using UnityEngine;
using System.Linq;

[CreateAssetMenu]
public class Plant : ScriptableObject
{
    [SerializeField] private float waterCapacity;
    [SerializeField] private float startingWater;
    [SerializeField] private WaterConsumption[] waterConsumptions;
    [SerializeField] private float wiltingSeconds;

    public float WaterCapacity => waterCapacity;
    public float StartingWater => startingWater;
    public float WaterConsumption(float water) => (waterConsumptions.Where(x => water >= x.Threshold).OrderByDesc(x => x.Threshold).FirstOrDefault() ?? waterConsumptions.First()).WaterConsumptionPerSecond;
    public float WiltingSeconds => wiltingSeconds;
}
