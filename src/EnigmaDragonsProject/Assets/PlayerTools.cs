using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerTools : MonoBehaviour
{
    [SerializeField] private WateringTool melee;
    [SerializeField] private WateringTool ranged;

    public void FillTolls()
    {
        melee.Fill();
        ranged.Fill();
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