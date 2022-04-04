using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Watering tool")]
public class WateringTool : ScriptableObject, IWaterHolder
{
    public string name;
    [PreviewField] public Sprite sprite;
    [SerializeField] private float startingAmount;
    [TextArea] public string description;
    public bool isRanged;
    [ShowIf("isRanged")] public float range;
    [ShowIf("isRanged")] public int maxCharges;
    [ShowIf("@!isRanged")] public float maxCapacity;
    public float waterTransferRate;
    [SerializeField] private int upgradeTier;
    [ShowInInspector] [ReadOnly] [ShowIf("@!isRanged")] private float currentWater;
    [ShowInInspector] [ReadOnly] [ShowIf("isRanged")] private int currentCharge;

    [SerializeField] private CurrentGameState gameState;
    public float WaterAmount => isRanged ? currentCharge : currentWater;
    public float MaxWaterAmount => isRanged ? maxCharges + gameState.State.playerStats.bonusCharges : maxCapacity * gameState.State.playerStats.capacity;
    
    public int UpgradeTier => upgradeTier;
    
    [Button]
    public void Fill()
    {
        if (isRanged)
        {
            currentCharge++;
            if(currentCharge > MaxWaterAmount) currentCharge = (int)MaxWaterAmount;
        }
        else
        {
            currentWater += (MaxWaterAmount * 0.1f)*gameState.State.playerStats.fillSpeed;
            if (currentWater > MaxWaterAmount) currentWater = MaxWaterAmount;
        }
    }

    /// <summary>
    /// Removes water from the tool
    /// Works only for ranged tools
    /// </summary>
    /// <param name="amount"></param>
    /// <returns>
    /// amount of water taken from the tool (to prevent it from going lower than 0) 
    /// </returns>
    [ShowIf("@!isRanged")]
    [Button(ButtonStyle.FoldoutButton)]
    public float UseWater(float amount)
    {
        if (isRanged) return 0f;
        
        if (amount <= currentWater)
        {
            currentWater -= amount;
            return amount;
        }
        else
        {
            amount = currentWater;
            currentWater = 0;
            return amount;
        }
    }

    public void UseCharge()
    {
        if (!isRanged) return;
        currentCharge -= 1;
    }

    [ShowIf("isRanged")]
    [Button(ButtonStyle.FoldoutButton)]
    public bool TryUseCharge()
    {
        if (currentCharge <= 0) return false;
        
        currentCharge--;
        return true;
    }

    public void Reset()
    {
        currentWater = startingAmount;
        currentCharge = (int)Math.Round(startingAmount);
    }

    public void Empty()
    {
        currentWater = 0;
    }
}