using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "playerTools")]
public class PlayerTools : ScriptableObject
{
    [SerializeField] private WateringTool startMelee;
    [SerializeField] private WateringTool startRanged;

    [ShowInInspector]
    [ReadOnly]
    private WateringTool melee;
    
    [ShowInInspector]
    [ReadOnly]
    private WateringTool ranged;

    [ShowInInspector]
    private float MeleeWater => melee ? melee.WaterAmount : 0f;
    [ShowInInspector]
    private float RangedWater => ranged ? ranged.WaterAmount : 0f;
    
    public void FillTolls()
    {
        if(melee) melee.Fill();
        if(ranged) ranged.Fill();
    }
    
    [Button]
    public void UpgradeMelee()
    {
        melee.TryUpgrade(out melee);
    }

    [Button]
    public void UpgradeRanged()
    {
        ranged.TryUpgrade(out ranged);
    }

    public WateringTool GetMeleeTool()
    {
        return melee;
    }
    
    public WateringTool GetRangedTool()
    {
        return ranged;
    }

    public void Reset()
    {
        melee = startMelee;
        ranged = startRanged;
        
        melee.Reset();
        ranged.Reset();
    }
}