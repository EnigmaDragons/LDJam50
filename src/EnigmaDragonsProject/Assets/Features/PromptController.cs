using TMPro;
using UnityEngine;

public class PromptController : OnMessage<ShowControlPrompt, HideControlPrompt>
{
    [SerializeField] private GameObject prompt;
    [SerializeField] private TextMeshProUGUI text;
    
    protected override void Execute(ShowControlPrompt msg)
    {
        text.text = msg.Prompt;
        prompt.SetActive(true);
    }

    protected override void Execute(HideControlPrompt msg)
    {
        prompt.SetActive(false);
    }
}