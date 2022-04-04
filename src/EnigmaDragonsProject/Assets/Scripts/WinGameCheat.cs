using UnityEngine;

public class WinGameCheat : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private Navigator navigator;
    
    public void Go()
    {
        gameState.UpdateState(s => s.Won = true);
        navigator.NavigateToVictoryScene();
    }
}
