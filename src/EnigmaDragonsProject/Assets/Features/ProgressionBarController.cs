using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionBarController : OnMessage<UpdateProgressionBar>
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;
    
    protected override void Execute(UpdateProgressionBar msg)
    {
        text.text = msg.Description;
        image.fillAmount = msg.Progress;
    }
}