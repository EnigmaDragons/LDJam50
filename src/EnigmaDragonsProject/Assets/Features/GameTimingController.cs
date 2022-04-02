using System;
using UnityEngine;

public class GameTimingController : MonoBehaviour
{
    [SerializeField] private float[] secondsTilNextEvent;
    [SerializeField] private CurrentGameState gameState;
    
    private int _eventIndex;
    private float _t;

    private void Update()
    {
        if (_eventIndex == secondsTilNextEvent.Length && !gameState.State.IsSpawning)
            return;
        _t += Time.deltaTime;
        if (_t >= secondsTilNextEvent[_eventIndex])
        {
            gameState.UpdateState(x => x.IsSpawning = true);
            Message.Publish(new SpawnNextSegment());
            _t -= secondsTilNextEvent[_eventIndex];
            _eventIndex++;
        }
        else if (gameState.State.IsWaterAboveCertainLevel(0.8f))
        {
            gameState.UpdateState(x => x.IsSpawning = true);
            Message.Publish(new SpawnNextSegment());
            _t = 0;
            _eventIndex++;
        }
    }
}