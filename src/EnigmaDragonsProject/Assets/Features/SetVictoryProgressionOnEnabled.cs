using System;
using UnityEngine;

public class SetVictoryProgressionOnEnabled : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;

    private void OnEnable()
    {
        gameState.UpdateState(x => x.VictoryProgressionEnabled = true);
    }
}