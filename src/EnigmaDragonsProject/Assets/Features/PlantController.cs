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
        _currentWater = plant.WaterCapacity;
        _wiltingSecondsRemaining = plant.WiltingSeconds;
    }
    
    private void Update()
    {
        if (_currentWater > 0)
        {
            _currentWater -= Time.deltaTime * plant.WaterConsumption;
            if (_currentWater < 0)
            {
                _wiltingSecondsRemaining += _currentWater / plant.WaterConsumption;
                _currentWater = 0;
            }
        }
        else 
            _wiltingSecondsRemaining -= Time.deltaTime;

        if (_wiltingSecondsRemaining <= 0)
            navigator.NavigateToGameOverScene();
    }
}