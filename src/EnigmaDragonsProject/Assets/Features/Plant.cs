using UnityEngine;
using System.Linq;

[CreateAssetMenu]
public class Plant : ScriptableObject
{
    [SerializeField] private float waterCapacity;
    [SerializeField] private float startingWater;
    [SerializeField] private WaterConsumption[] waterConsumptions;
    [SerializeField] private float wiltingSeconds;
    [SerializeField] private string incomingDescription;
    [SerializeField] private GameObject prefab;
    [SerializeField, TextArea(2, 4)] private string deadDescription;

    public float WaterCapacity => waterCapacity;
    public float StartingWater => startingWater;
    public float WaterConsumption(float water) => (waterConsumptions.Where(x => water >= x.Threshold).OrderByDescending(x => x.Threshold).FirstOrDefault() ?? waterConsumptions.First()).WaterConsumptionPerSecond;
    public float WiltingSeconds => wiltingSeconds;
    public string IncomingDescription => incomingDescription;
    public GameObject Prefab => prefab;
    public string DeadDescription => deadDescription;
}
