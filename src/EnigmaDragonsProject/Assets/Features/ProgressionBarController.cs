using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionBarController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;
    [SerializeField] private CurrentGameState gameState;
    
    private void Update()
    {
        text.text = gameState.State.progressionDescription;
        image.fillAmount = 1 - gameState.State.progress;
    }
}