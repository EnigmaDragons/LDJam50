using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    [SerializeField] private Plant plant;
    [SerializeField] private Navigator navigator;
    [SerializeField] private CurrentGameState gameState;

    [ShowInInspector] [ReadOnly] private int _id;
    
    private void Start()
    {
        gameState.UpdateState(x => _id = x.InitPlant(transform,  plant.StartingWater, plant.WiltingSeconds));
    }
    
    private void Update()
    {
        var plantState = gameState.State.PlantById(_id);
        var water = plantState.Water;
        var wiltingSecondsRemaining = plantState.WiltingRemainingSeconds;
            
        if (plantState.Water > 0)
        {
            var waterConsumption = plant.WaterConsumption(water);
            water -= Time.deltaTime * waterConsumption;
            if (water < 0)
            {
                wiltingSecondsRemaining += water / waterConsumption;
                water = 0;
            }
        }
        else 
            wiltingSecondsRemaining -= Time.deltaTime;

        gameState.UpdateState(x =>
        {
            plantState.Water = water;
            plantState.WiltingRemainingSeconds = wiltingSecondsRemaining;
        });
        
        if (wiltingSecondsRemaining <= 0)
            navigator.NavigateToGameOverScene();
    }

    public float AddWater(float amount)
    {
        float waterConsumed = 0;
        gameState.UpdateState(x =>
        {
            var plantState = x.PlantById(_id);
            waterConsumed = Math.Min(plant.WaterCapacity - plantState.Water, amount);
            plantState.Water += waterConsumed;
        });
        return waterConsumed;
    }
}