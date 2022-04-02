using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerTools : MonoBehaviour
{
    [SerializeField] private WateringTool melee;
    [SerializeField] private WateringTool ranged;

    [ShowInInspector]
    private float MeleeWater => melee ? melee.WaterAmount : 0f;
    [ShowInInspector]
    private float RangedWater => ranged ? ranged.WaterAmount : 0f;

    public void FillTolls()
    {
        print("fill");
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
}