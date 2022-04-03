using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Water.Upgrades
{
    public class PlayerUpgrades : ScriptableObject
    {
        [SerializeField] private List<BasePlayerUpgrade> availableUpgrades;
        
        private List<BasePlayerUpgrade> currentUpgrades;

        public List<BasePlayerUpgrade> GetUpgradeSelection()
        {
            var available = availableUpgrades.Where(upg => upg.CanAppear(currentUpgrades));
            var rand = new System.Random();
            available = available.OrderBy(i => rand.Next());
            available = available.Take(3);
            return available.ToList();
        }
    }
}
