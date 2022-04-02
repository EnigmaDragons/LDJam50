using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarUI : MonoBehaviour
{
    [SerializeField] private PlayerTools playerTools;
    [SerializeField] private Image meleeImage;
    [SerializeField] private Image rangedImage;
    
    [SerializeField] private Slider meleeSlider;
    [SerializeField] private Slider rangedSlider;

    private void Update()
    {
        meleeImage.sprite = playerTools.GetMeleeTool().sprite;
        meleeSlider.maxValue = playerTools.GetMeleeTool().MaxWaterAmount;
        meleeSlider.value = playerTools.GetMeleeTool().WaterAmount;
        
        rangedImage.sprite = playerTools.GetRangedTool().sprite;
        rangedSlider.maxValue = playerTools.GetRangedTool().MaxWaterAmount;
        rangedSlider.value = playerTools.GetRangedTool().WaterAmount;
    }
}
