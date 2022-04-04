using UnityEngine;

public class EnabledIfGameWon : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;

    private void Awake()
    {
        gameObject.SetActive(gameState.State.Won);
    }
}
