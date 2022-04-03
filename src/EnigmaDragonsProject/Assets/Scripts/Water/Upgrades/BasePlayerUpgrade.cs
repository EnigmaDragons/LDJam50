using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Water.Upgrades
{
    public abstract class BasePlayerUpgrade : ScriptableObject
    {
        [SerializeField] private string name;
        [PreviewField]
        [SerializeField] private Sprite icon;
        [TextArea]
        [SerializeField] private string description;
        [SerializeField] private List<BasePlayerUpgrade> requirements;
        [SerializeField] protected CurrentGameState gameState;
        
        public Sprite Icon => icon;
        public string Name => name;
        public string Description => description;

        public bool CanAppear(List<BasePlayerUpgrade> currentUpgrades)
        {
            var req = requirements.Except(currentUpgrades);
            return !req.Any();
        }

        public abstract void OnUpgradeBought();

    }
}
