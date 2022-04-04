using System;
using System.Linq;
using UnityEngine;

public class SwapPlacesWithAPlantAndAddWater : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private PlantController plant;
    [SerializeField] private GameObject particleEffect;
    [SerializeField] private float timeToSwap = 15;
    [SerializeField] private float waterGivenOnSwap = 5;

    private float _timeRemainingBeforeSwap;

    private void Start() => _timeRemainingBeforeSwap = timeToSwap;
    
    private void Update()
    {
        _timeRemainingBeforeSwap -= Time.deltaTime;
        if (_timeRemainingBeforeSwap <= 0)
        {
            _timeRemainingBeforeSwap += timeToSwap;
            var otherPlant = gameState.State.Plants.Where(x => x.Id != plant.Id && x.Name != "GoldenAppleTree").Random();
            var yourLocation = transform.position;
            var otherLocation = otherPlant.Transform.position;
            transform.position = otherLocation;
            otherPlant.Transform.position = yourLocation;
            gameState.UpdateState(x =>
            {
                otherPlant.Water = Math.Min(otherPlant.WaterCapacity, otherPlant.Water + waterGivenOnSwap);
                x.PlantById(plant.Id).Water = Math.Min(otherPlant.WaterCapacity, otherPlant.Water + waterGivenOnSwap);
            });
            Instantiate(particleEffect, yourLocation, Quaternion.identity);
            Instantiate(particleEffect, otherLocation, Quaternion.identity);
        }
    }
}