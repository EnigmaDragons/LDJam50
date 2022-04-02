using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public sealed class GameState
{
    public bool IsSpawning;
    
    private List<PlantState> _plantStates;
    private int _currentId;

    public GameState()
    {
        _plantStates = new List<PlantState>();
    }

    public bool IsWaterAboveCertainLevel(float percent) => _plantStates.All(x => x.Water > x.WaterCapacity * percent);  
    
    public int InitPlant(Transform transform, float startingWater, float wiltingSeconds, float waterCapacity)
    {
        _currentId++;
        _plantStates.Add(new PlantState { Id = _currentId, Transform = transform, Water = startingWater, WiltingRemainingSeconds = wiltingSeconds, WaterCapacity = waterCapacity });
        return _currentId;
    }

    public PlantState PlantById(int id) => _plantStates.First(x => x.Id == id);
}
