using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Watering tool")]
public class WateringTool : ScriptableObject, IWaterHolder
{
    public string name;
    
    [PreviewField]
    public Sprite sprite;

    [TextArea]
    public string description;

    public bool isRanged;
    [ShowIf("isRanged")]
    public float range;
    [ShowIf("isRanged")]
    public int maxCharges;
    
    [ShowIf("@!isRanged")]
    public float maxCapacity;
    [ShowIf("@!isRanged")]
    public float waterTransferRate;

    
    [SerializeField] private bool upgradable;
    [ShowIf("upgradable")]
    [SerializeField] private WateringTool upgradesInto;
    
    [ShowInInspector]
    [ReadOnly]
    [ShowIf("@!isRanged")]
    private float currentWater;
    
    [ShowInInspector]
    [ReadOnly]
    [ShowIf("isRanged")]
    private int currentCharge;
    
    public bool TryUpgrade(out WateringTool upgrade)
    {
        upgrade = this;
        if (!upgradable) return false;
        
        upgrade = upgradesInto;
        return true;
    }

    public float WaterAmount => isRanged ? currentCharge : currentWater;
    public float MaxWaterAmount => isRanged ? maxCharges : maxCapacity;

    [Button]
    public void Fill()
    {
        if (isRanged)
        {
            currentCharge++;
            if(currentCharge > maxCharges) currentCharge = maxCharges;
        }
        else
        {
            currentWater += maxCapacity * 0.1f;
            if (currentWater > maxCapacity) currentWater = maxCapacity;
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

    [ShowIf("isRanged")]
    [Button(ButtonStyle.FoldoutButton)]
    public bool TryUseCharge()
    {
        if (currentCharge <= 0) return false;
        
        currentCharge--;
        return true;
    }
    
    
    
    
}