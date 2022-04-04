using Sirenix.OdinInspector;
using UnityEngine;

public class GameOverCheat : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private Navigator navigator;
    [SerializeField] private Plant plantWhoDied;

    [Button]
    public void Lose()
    {
        gameState.UpdateState(s =>
        {
            s.Lost = true;
            s.PlantWhoDied = plantWhoDied;
        });
        navigator.NavigateToGameOverScene();
    }
}
