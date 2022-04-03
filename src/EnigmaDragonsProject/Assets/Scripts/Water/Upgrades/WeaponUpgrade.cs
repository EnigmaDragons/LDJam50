using UnityEngine;

namespace Water.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/WeaponUpgrade")]
    public class WeaponUpgrade : BasePlayerUpgrade
    {
        [SerializeField] private bool ranged;
        [SerializeField] private WateringTool upgrade;
    }
}