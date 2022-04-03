using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class PlantController : MonoBehaviour
{
    [SerializeField] private Plant plant;
    [SerializeField] private Navigator navigator;
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private Image waterFill;
    [SerializeField] private Image wiltingFill;

    [ShowInInspector] [ReadOnly] private int _id;
    public int Id => _id;

    private void Start()
    {
        gameState.UpdateState(x => _id = x.InitPlant(transform,  plant.StartingWater, plant.WiltingSeconds, plant.WaterCapacity));
        if (waterFill == null)
            Log.Error($"{name} does not have a Water Fill UI");
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
                if (wiltingFill != null)
                {
                    wiltingFill.fillAmount = wiltingSecondsRemaining / plant.WiltingSeconds;
                    wiltingFill.color = new Color(1, 1, 1, 1);
                }
                water = 0;
            }
            if (waterFill != null)
                waterFill.fillAmount = water / plantState.WaterCapacity;
        }
        else
        {
            wiltingSecondsRemaining -= Time.deltaTime;
            if (wiltingFill != null)
            {
                wiltingFill.fillAmount = wiltingSecondsRemaining / plant.WiltingSeconds;  
                wiltingFill.color = new Color(1, 1, 1, 1);
            }
        }

        gameState.UpdateState(x =>
        {
            plantState.Water = water;
            plantState.WiltingRemainingSeconds = wiltingSecondsRemaining;
        });

        if (wiltingSecondsRemaining <= 0 && !gameState.State.Lost)
        {
            gameState.UpdateState(x => x.Lost = true);
            navigator.NavigateToGameOverScene();   
        }
    }

    public float AddWater(float amount)
    {
        float waterConsumed = 0;
        gameState.UpdateState(x =>
        {
            var plantState = x.PlantById(_id);
            waterConsumed = Math.Min(plant.WaterCapacity - plantState.Water, amount);
            plantState.Water += waterConsumed;
            if (plantState.Water > 0 && wiltingFill != null)
                wiltingFill.color = new Color(1, 1, 1, 0.5f);
        });
        return waterConsumed;
    }
}