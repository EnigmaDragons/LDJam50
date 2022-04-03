using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Water.Upgrades;

public class UpgradeCard : MonoBehaviour
{
    [SerializeField] public BasePlayerUpgrade upgrade;
    [SerializeField] private Image upgradeIcon;
    [SerializeField] private TextMeshProUGUI upgradeText;

    public Action<BasePlayerUpgrade> onUpgradeHover;
    public Action<BasePlayerUpgrade> onUpgradeStopHover;
    public Action<BasePlayerUpgrade> onUpgradeSelected;
    
    private void Update()
    {
        upgradeIcon.sprite = upgrade.Icon;
        upgradeText.text = upgrade.Name;
    }

    public void OnUpgradeHover() => onUpgradeHover?.Invoke(upgrade);
    public void OnUpgradeStopHover() => onUpgradeStopHover?.Invoke(upgrade);
    public void OnUpgradeSelected() => onUpgradeSelected?.Invoke(upgrade);


}
