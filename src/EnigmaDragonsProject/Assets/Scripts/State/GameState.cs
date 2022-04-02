using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public sealed class GameState
{
    private List<PlantState> _plantStates;
    private int _currentId;

    public GameState()
    {
        _plantStates = new List<PlantState>();
    }
    
    public int InitPlant(Transform transform, float startingWater, float wiltingSeconds)
    {
        _currentId++;
        _plantStates.Add(new PlantState { Id = _currentId, Transform = transform, Water = startingWater, WiltingRemainingSeconds = wiltingSeconds });
        return _currentId;
    }

    public PlantState PlantById(int id) => _plantStates.First(x => x.Id == id);
}
