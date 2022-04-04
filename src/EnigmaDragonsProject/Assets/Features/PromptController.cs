using TMPro;
using UnityEngine;

public class PromptController : OnMessage<ShowControlPrompt, HideControlPrompt>
{
    [SerializeField] private GameObject prompt;
    [SerializeField] private TextMeshProUGUI text;

    private int _timesShowed;
    
    protected override void Execute(ShowControlPrompt msg)
    {
        if (_timesShowed > 20)
            return;
        text.text = msg.Prompt;
        prompt.SetActive(true);
        _timesShowed++;
    }

    protected override void Execute(HideControlPrompt msg)
    {
        prompt.SetActive(false);
    }
}