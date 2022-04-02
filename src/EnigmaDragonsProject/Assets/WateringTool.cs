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
    public float maxCapacity;
    
    [SerializeField] private bool upgradable;
    [ShowIf("upgradable")]
    [SerializeField] private WateringTool upgradesInto;
    
    private float currentWater;

    public bool TryUpgrade(out WateringTool upgrade)
    {
        upgrade = null;
        if (!upgradable) return false;
        
        upgrade = upgradesInto;
        return true;
    }
    
    public float WaterAmount { get => currentWater; set => currentWater = value; }
}