using System.Linq;
using TMPro;
using UnityEngine;

public class ProgressionBarController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI victoryText;
    [SerializeField] private GameObject normal;
    [SerializeField] private GameObject victory;
    [SerializeField] private RectTransform image;
    [SerializeField] private RectTransform victoryImage;
    [SerializeField] private CurrentGameState gameState;

    private bool _isVictory;
    
    private void Update()
    {
        if (gameState.State.VictoryProgressionEnabled && !_isVictory)
        {
            _isVictory = true;
            normal.SetActive(false);
            victory.SetActive(true);
        }

        if (_isVictory)
        {
            victoryText.text = gameState.State.progressionDescription;
            var appleTree = gameState.State.Plants.FirstOrDefault(x => x.Name == "GoldenAppleTree");
            victoryImage.localScale = new Vector3(appleTree == null ? 0 : appleTree.Water / appleTree.WaterCapacity, image.localScale.y, image.localScale.z);
        }
        else
        {
            text.text = gameState.State.progressionDescription;
            image.localScale = new Vector3(1 - gameState.State.progress, image.localScale.y, image.localScale.z);
        }
        
        
        
    }
}