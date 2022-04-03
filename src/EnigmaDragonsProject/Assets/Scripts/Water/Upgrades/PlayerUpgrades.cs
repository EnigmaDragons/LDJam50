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
            var rand = new System.Random();
            available = available.OrderBy(i => rand.Next());
            available = available.Take(3);
            return available.ToList();
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
