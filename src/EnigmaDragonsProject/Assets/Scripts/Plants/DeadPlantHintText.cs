
using TMPro;
using UnityEngine;

public class DeadPlantHintText : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private TextMeshProUGUI label;

    private void Awake()
    {
        var plant = gameState.State.PlantWhoDied;
        if (plant == null)
            return;

        label.text = plant.DeadDescription;
    }
}