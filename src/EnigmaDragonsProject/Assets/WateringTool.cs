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
    
    private float currentWater;
    private int currentCharge;
    
    public bool TryUpgrade(out WateringTool upgrade)
    {
        upgrade = this;
        if (!upgradable) return false;
        
        upgrade = upgradesInto;
        return true;
    }

    public float WaterAmount => isRanged ? currentCharge : currentWater;
    public float MaxWaterAmount => isRanged ? maxCapacity : maxCharges;

    public void Fill()
    {
        if (isRanged) currentCharge++;
        else
        {
            currentWater += MaxWaterAmount * 0.1f;
            if (currentWater > MaxWaterAmount) currentWater = MaxWaterAmount;
        }
    }
}