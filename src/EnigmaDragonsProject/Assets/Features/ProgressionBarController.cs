using TMPro;
using UnityEngine;

public class ProgressionBarController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private RectTransform image;
    [SerializeField] private CurrentGameState gameState;
    
    private void Update()
    {
        text.text = gameState.State.progressionDescription;
        image.localScale = new Vector3(1 - gameState.State.progress, image.localScale.y, image.localScale.z);
    }
}