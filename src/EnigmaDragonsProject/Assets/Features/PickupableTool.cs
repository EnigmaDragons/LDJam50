using UnityEngine;

public class PickupableTool : MonoBehaviour
{
    [SerializeField] private WateringTool tool;
    [SerializeField] private CurrentGameState gameState;
    
    private void OnTriggerEnter(Collider other)
    {
        gameState.State.TryUpgrade(tool);
        gameObject.SetActive(false);
    }
}