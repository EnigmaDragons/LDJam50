using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Water.Upgrades
{
    [CreateAssetMenu(menuName = "OneTime/PlayerUpgrades")]
    public class PlayerUpgrades : ScriptableObject
    {
        [SerializeField] private List<BasePlayerUpgrade> availableUpgrades;
        
        [ShowInInspector]
        [ReadOnly]
        private List<BasePlayerUpgrade> currentUpgrades;

        public List<BasePlayerUpgrade> GetUpgradeSelection()
        {
            var available = availableUpgrades.Except(currentUpgrades);
            available = available.Where(upg => upg.CanAppear(currentUpgrades));
            var rand = new System.Random();
            available = available.OrderBy(i => rand.Next());
            available = available.Take(3);
            return available.ToList();
        }

        public void UnlockUpgrade(BasePlayerUpgrade upgrade)
        {
            currentUpgrades.Add(upgrade);
        }

        [Button]
        public void ResetUpgrades()
        {
            currentUpgrades.Clear();   
        }
    }
}
