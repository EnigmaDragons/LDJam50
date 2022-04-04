using Sirenix.OdinInspector;
using UnityEngine;

namespace Water.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/StatUpgrade")]
    public class StatUpgrade : BasePlayerUpgrade
    {
        [InfoBox("Show how each parameter will change after buying an upgrade, new stats are computed as: newStat = oldStat + thisChange")]
        [SerializeField] private PlayerStats statsChange;
        public override void OnUpgradeBought()
        {
            gameState.UpdateState(x => x.playerStats += statsChange);
        }

        public override string UpgradeType() => "Stat";
    }
}