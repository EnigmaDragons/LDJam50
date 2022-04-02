using UnityEngine;

public class PlantController : MonoBehaviour
{
    [SerializeField] private Plant plant;
    [SerializeField] private float currentWater;
    [SerializeField] private float healthInSeconds;
    [SerializeField] private Navigator navigator;

    private void Awake() => currentWater = plant.WaterCapacity;
    
    private void Update()
    {
        if(currentWater > 0)
            currentWater -= Time.deltaTime * plant.WaterConsumption;
        else if(healthInSeconds > 0)
            healthInSeconds -= Time.deltaTime;
        else
            navigator.NavigateToGameOverScene();
    }
}