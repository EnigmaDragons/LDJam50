using UnityEngine;

namespace Water.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/WeaponUpgrade")]
    public class WeaponUpgrade : BasePlayerUpgrade
    {
        [SerializeField] private WateringTool upgrade;
        public override void OnUpgradeBought()
        {
            if(!upgrade.isRanged) gameState.UpdateState(t =>
            {
                t.MeleeTool = upgrade;
            });
            else gameState.UpdateState(t =>
            {
                t.RangedTool = upgrade;
            });
        }
    }
}