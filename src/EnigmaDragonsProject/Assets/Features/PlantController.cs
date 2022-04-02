using System;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    [SerializeField] private Plant plant;
    [SerializeField] private Navigator navigator;

    private float _currentWater;
    private float _wiltingSecondsRemaining;
    
    private void Awake()
    {
        _currentWater = plant.StartingWater;
        _wiltingSecondsRemaining = plant.WiltingSeconds;
    }
    
    private void Update()
    {
        if (_currentWater > 0)
        {
            var waterConsumption = plant.WaterConsumption(_currentWater);
            _currentWater -= Time.deltaTime * waterConsumption;
            if (_currentWater < 0)
            {
                _wiltingSecondsRemaining += _currentWater / waterConsumption;
                _currentWater = 0;
            }
        }
        else 
            _wiltingSecondsRemaining -= Time.deltaTime;

        if (_wiltingSecondsRemaining <= 0)
            navigator.NavigateToGameOverScene();
    }
}