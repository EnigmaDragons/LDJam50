using System;
using System.Collections.Generic;
using System.Linq;
using Codice.CM.Common.Serialization.Replication;
using UnityEngine;
using Water.Upgrades;

[Serializable]
public sealed class GameState
{
    public List<BasePlayerUpgrade> upgrades;
    private List<PlantState> _plantStates;
    private int _currentId;
    public WateringTool MeleeTool;
    public WateringTool RangedTool;
    public PlayerStats playerStats;
    public bool Lost;
    public bool Won;
    public string progressionDescription;
    public float progress;
    public bool GiveUpgrade;

    public GameState()
    {
        playerStats = new PlayerStats();
        upgrades = new List<BasePlayerUpgrade>();
        _plantStates = new List<PlantState>();
        _currentId = 0;
        Lost = false;
        Won = false;
    }

    public bool IsWaterAboveCertainLevel(float percent) => _plantStates.All(x => x.Water > x.WaterCapacity * percent);  
    
    public int InitPlant(Transform transform, float startingWater, float wiltingSeconds, float waterCapacity)
    {
        _currentId++;
        _plantStates.Add(new PlantState { Id = _currentId, Transform = transform, Water = startingWater, WiltingRemainingSeconds = wiltingSeconds, WaterCapacity = waterCapacity });
        return _currentId;
    }

    public PlantState PlantById(int id) => _plantStates.First(x => x.Id == id);

    public void TryUpgrade(WateringTool tool)
    {
        tool.Reset();
        if (tool.isRanged && tool.UpgradeTier > RangedTool.UpgradeTier)
            RangedTool = tool;
        else if (!tool.isRanged && tool.UpgradeTier > MeleeTool.UpgradeTier)
            MeleeTool = tool;
    }
}