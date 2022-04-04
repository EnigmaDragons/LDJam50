using System;
using System.Collections.Generic;
using System.Linq;
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
    public Plant PlantWhoDied;
    public bool Won;
    public string progressionDescription;
    public float progress;
    public bool GiveUpgrade;
    public bool WaterBalloonUnlocked => playerAbilities.WaterBalloon;
    public float WaterBalloonCooldown;
    public PlayerAbilities playerAbilities;
    public bool PlantSpawningComplete = false;

    public GameState()
    {
        playerStats = new PlayerStats();
        upgrades = new List<BasePlayerUpgrade>();
        _plantStates = new List<PlantState>();
        _currentId = 0;
        Lost = false;
        Won = false;
        GiveUpgrade = false;
        playerAbilities = new PlayerAbilities();
        PlantWhoDied = null;
    }

    public bool IsWaterAboveCertainLevel(float percent) => _plantStates.All(x => x.Water > x.WaterCapacity * percent);  
    
    public int InitPlant(Transform transform, float startingWater, float wiltingSeconds, float waterCapacity)
    {
        _currentId++;
        _plantStates.Add(new PlantState { Id = _currentId, Transform = transform, Water = startingWater, WiltingRemainingSeconds = wiltingSeconds, WaterCapacity = waterCapacity });
        return _currentId;
    }

    public PlantState PlantById(int id) => _plantStates.FirstOrDefault(x => x.Id == id) ?? new PlantState { Id = -1 };

    public void TryUpgrade(WateringTool tool)
    {
        tool.Reset();
        if (tool.isRanged && tool.UpgradeTier > RangedTool.UpgradeTier)
            RangedTool = tool;
        else if (!tool.isRanged && tool.UpgradeTier > MeleeTool.UpgradeTier)
            MeleeTool = tool;
    }

    public float GetSpellCooldown(string spellName)
    {
        if (spellName == "Water Balloon")
            return WaterBalloonCooldown;
        return 0;
    }

    public void SetSpellCooldown(string spellName, float cooldown)
    {
        if (spellName == "Water Balloon")
            WaterBalloonCooldown = cooldown;
    }

    public void UnlockSpell(string spellName)
    {
        switch (spellName)
        {
            case "Water Balloon":
                playerAbilities.WaterBalloon = true;
                break;
            case "Dash":
                playerAbilities.Dash = true;
                break;
        }
    }

    public IEnumerable<PlantState> Plants => _plantStates;
}

[Serializable]
public class PlayerAbilities
{
    public bool WaterBalloon;
    public bool Dash;

}