using UnityEngine;

namespace Water.Upgrades
{
    [CreateAssetMenu(menuName = "Upgrades/Ability")]
    public class AbilityUpgrade : BasePlayerUpgrade
    {
        [SerializeField] private string spellName;
        
        public override void OnUpgradeBought()
        {
            gameState.UpdateState(x => x.UnlockSpell(spellName));
        }

        public override string UpgradeType() => "Spell";
    }
}