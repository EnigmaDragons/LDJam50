using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellController : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private string spellName;
    
    private bool _isUnlocked;
    
    public void Awake()
    {
        ui.SetActive(false);
        timer.text = "";
        icon.color = new Color(1, 1, 1, 1);
    }

    private void Update()
    {
        if (!gameState.State.WaterBalloonUnlocked)
            return;
        if (!_isUnlocked)
            ui.SetActive(true);
        if (gameState.State.GetSpellCooldown(spellName) > 0)
        {
            gameState.UpdateState(x => x.SetSpellCooldown(spellName, Math.Max(0, gameState.State.GetSpellCooldown(spellName) - Time.deltaTime)));
            if (gameState.State.GetSpellCooldown(spellName) <= 0)
            {
                icon.color = new Color(1, 1, 1, 1);
                timer.text = "";
            }
            else
            {
                icon.color = new Color(1, 1, 1, 0.5f);
                timer.text = gameState.State.GetSpellCooldown(spellName).ToString("0.0");   
            }
        }
    }
}