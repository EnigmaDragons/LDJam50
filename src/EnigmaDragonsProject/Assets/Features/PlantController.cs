using System;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    [SerializeField] private Plant plant;
    [SerializeField] private Navigator navigator;

    private float _currentWater;
    private float _wiltingSeconds;
    
    private void Awake()
    {
        _currentWater = plant.WaterCapacity;
        _wiltingSeconds = plant.WiltingSeconds;
    }
    
    private void Update()
    {
        if (_currentWater > 0)
        {
            _currentWater -= Time.deltaTime * plant.WaterConsumption;
            if (_currentWater < 0)
            {
                _wiltingSeconds += _currentWater / plant.WaterConsumption;
                _currentWater = 0;
            }
        }
        else 
            _wiltingSeconds -= Time.deltaTime;

        if (_wiltingSeconds <= 0)
            navigator.NavigateToGameOverScene();
    }
}