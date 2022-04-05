using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Water.Upgrades;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private PlayerUpgrades playerUpgrades;
    [SerializeField] private GameObject upgradeUI;
    [AssetsOnly]
    [SerializeField] private GameObject upgradeCardPrefab;
    [SerializeField] private Transform upgradeCardParent;
    [SerializeField] private TextMeshProUGUI upgradeDescriptionText;
    [SerializeField] private CurrentGameState gameState;
    private Camera mainCamera;
    
    private void Awake()
    {
        mainCamera = Camera.main;
        CloseUpgradeUI();
    }

    private void Update()
    {
        if (gameState.State.GiveUpgrade)
        {
            ShowUpgradeSelection();
            gameState.UpdateState(x => x.GiveUpgrade = false);
        }
    }

    public void UnlockUpgrade(BasePlayerUpgrade upgrade)
    {
        playerUpgrades.UnlockUpgrade(upgrade);
        upgrade.OnUpgradeBought();
        CloseUpgradeUI();
        Message.Publish(new PlaySoundRequested(GameSounds.ToolUpgrade, mainCamera.transform.position));
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
        
        var selection = playerUpgrades.GetUpgradeSelection();
        if (selection.Count == 0) return;
        
        OpenUpgradeUI();
        
        foreach (var upgrade in selection)
        {
            SpawnUpgrade(upgrade);
        }
    }
}
