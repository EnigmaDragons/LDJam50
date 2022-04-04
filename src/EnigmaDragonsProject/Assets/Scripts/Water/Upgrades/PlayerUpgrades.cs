using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Water.Upgrades
{
    [CreateAssetMenu(menuName = "OneTime/PlayerUpgrades")]
    public class PlayerUpgrades : ScriptableObject
    {
        [SerializeField] private CurrentGameState gameState;
        [SerializeField] private List<BasePlayerUpgrade> availableUpgrades;
        
        [ShowInInspector]
        [ShowIf("@gameState != null")]
        private List<BasePlayerUpgrade> CurrentUpgrades => gameState.State.upgrades;

        public List<BasePlayerUpgrade> GetUpgradeSelection()
        {
            var available = availableUpgrades.Except(CurrentUpgrades);
            available = available.Where(upg => upg.CanAppear(CurrentUpgrades));
            var chosen = new List<BasePlayerUpgrade>();
            var rand = new System.Random(Guid.NewGuid().GetHashCode());
            available = available.OrderBy(i => rand.Next()).ToArray();
            while (chosen.Count < 3 && chosen.Count != available.Count())
            {
                var nextUpgradeWanted = available.FirstOrDefault(x => chosen.All(up => up.UpgradeType() != x.UpgradeType()));
                if (nextUpgradeWanted == null)
                    nextUpgradeWanted = available.FirstOrDefault(x => chosen.All(up => up != x));
                chosen.Add(nextUpgradeWanted);
            }
            return chosen;
        }

        public void UnlockUpgrade(BasePlayerUpgrade upgrade)
        {
            CurrentUpgrades.Add(upgrade);
        }

        [Button]
        public void ResetUpgrades()
        {
            CurrentUpgrades.Clear();   
        }
    }
}
