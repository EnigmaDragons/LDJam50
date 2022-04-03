using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Water.Upgrades;

public class UpgradeUI : OnMessage<ShowUpgrade>
{
    [SerializeField] private PlayerUpgrades playerUpgrades;
    [SerializeField] private GameObject upgradeUI;
    [AssetsOnly]
    [SerializeField] private GameObject upgradeCardPrefab;
    [SerializeField] private Transform upgradeCardParent;
    [SerializeField] private TextMeshProUGUI upgradeDescriptionText;


    private void Awake()
    {
        CloseUpgradeUI();
    }

    public void UnlockUpgrade(BasePlayerUpgrade upgrade)
    {
        playerUpgrades.UnlockUpgrade(upgrade);
        upgrade.OnUpgradeBought();
        CloseUpgradeUI();
    }
    
    public void ShowDescription(BasePlayerUpgrade upgrade)
    {
        upgradeDescriptionText.text = upgrade.Description;
    }

    public void HideUpgradeDescription(BasePlayerUpgrade upgrade = null)
    {
        upgradeDescriptionText.text = "";
    }

    private void SpawnUpgrade(BasePlayerUpgrade upgrade)
    {
        var inst = Instantiate(upgradeCardPrefab, upgradeCardParent);
        var card = inst.GetComponent<UpgradeCard>();
        card.upgrade = upgrade;
        card.onUpgradeHover += ShowDescription;
        card.onUpgradeStopHover += HideUpgradeDescription;
        card.onUpgradeSelected += UnlockUpgrade;
    }

    private void DestroyUpgrades()
    {
        for (int i = 0; i < upgradeCardParent.childCount; i++)
        {
            Destroy(upgradeCardParent.GetChild(i).gameObject);
        }
    }

    public void OpenUpgradeUI()
    {
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
    }
    
    [Button]
    public void ShowUpgradeSelection()
    {
        HideUpgradeDescription();
        DestroyUpgrades();
        
        OpenUpgradeUI();
        
        var selection = playerUpgrades.GetUpgradeSelection();
        foreach (var upgrade in selection)
        {
            SpawnUpgrade(upgrade);
        }
    }


    protected override void Execute(ShowUpgrade msg) => ShowUpgradeSelection();
}
