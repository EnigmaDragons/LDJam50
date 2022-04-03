using System;
using UnityEngine;

public class WinGameOnPlantFilled : OnMessage<GameStateChanged>
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private Navigator navigator;

    private int _id;
    
    protected override void Execute(GameStateChanged msg)
    {
        EnsureInitialized();
        if (!gameState.State.Won && _id > 0 && Math.Abs(gameState.State.PlantById(_id).Water - gameState.State.PlantById(_id).WaterCapacity) < 0.1)
        {
            gameState.State.Won = true;
            navigator.NavigateToVictoryScene();   
        }
    }

    private void EnsureInitialized()
    {
        if (_id == 0)
            _id = GetComponent<PlantController>().Id;
    }
}