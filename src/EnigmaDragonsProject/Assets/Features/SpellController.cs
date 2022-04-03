using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellController : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI timer;

    private bool _enabled;
    private float _cooldownTime;

    public bool IsAvailable() => _cooldownTime <= 0;

    public void Use(float cooldown)
    {
        _cooldownTime = cooldown;
        timer.text = _cooldownTime.ToString("0.0");
        icon.color = new Color(1, 1, 1, 0.5f);
    }
    
    public void Init()
    {
        if (_enabled)
            return;
        _enabled = true;
        ui.SetActive(true);
        timer.text = "";
        icon.color = new Color(1, 1, 1, 1);
    }

    private void Update() 
    {
        if (_cooldownTime > 0)
        {
            _cooldownTime = Math.Max(0, _cooldownTime - Time.deltaTime);
            if (_cooldownTime <= 0)
            {
                icon.color = new Color(1, 1, 1, 1);
                timer.text = "";
            }
            else
                timer.text = _cooldownTime.ToString("0.0");
        }
    }
}