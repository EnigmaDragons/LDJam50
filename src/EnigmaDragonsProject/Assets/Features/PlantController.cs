using UnityEngine;

public class PlantController : MonoBehaviour
{
    [SerializeField] private Plant plant;
    [SerializeField] private float currentWater;
    [SerializeField] private Navigator navigator;

    private void OnAwake() => currentWater = plant.WaterCapacity;
    
    private void Update()
    {
        currentWater -= Time.deltaTime * plant.WaterConsumption;
        if (currentWater <= 0)
            navigator.NavigateToGameOverScene();
    }
}