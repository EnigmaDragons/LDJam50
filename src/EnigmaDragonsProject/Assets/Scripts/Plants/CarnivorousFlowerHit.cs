using UnityEngine;

public class CarnivorousFlowerHit : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameState.State.MeleeTool != null)
                gameState.State.MeleeTool.Empty();
            if (gameState.State.RangedTool != null)
                gameState.State.RangedTool.Empty();
        }
    }
}
