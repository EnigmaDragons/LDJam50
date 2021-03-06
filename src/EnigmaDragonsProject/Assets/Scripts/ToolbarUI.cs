using UnityEngine;
using UnityEngine.UI;

public class ToolbarUI : MonoBehaviour
{
    [SerializeField] private PlayerTools playerTools;
    [SerializeField] private Image meleeImage;
    [SerializeField] private Image rangedImage;
    [SerializeField] private GameObject meleePanel;
    [SerializeField] private GameObject rangedPanel;
    
    [SerializeField] private Slider meleeSlider;
    [SerializeField] private Slider rangedSlider;

    private void Update()
    {
        var meleeTool = playerTools.GetMeleeTool();
        if (meleeTool != null)
        {
            meleeImage.sprite = meleeTool.sprite;
            meleeSlider.maxValue = meleeTool.MaxWaterAmount;
            meleeSlider.value = meleeTool.WaterAmount;
        }
        else
        {
            meleePanel.SetActive(false);
        }

        var rangedTool = playerTools.GetRangedTool();
        if (rangedTool != null)
        {
            rangedPanel.SetActive(true);
            rangedImage.sprite = rangedTool.sprite;
            rangedSlider.maxValue = rangedTool.MaxWaterAmount;
            rangedSlider.value = rangedTool.WaterAmount;
        }
        else
        {
            rangedPanel.SetActive(false);
        }
    }
}
