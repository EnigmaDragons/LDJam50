using UnityEngine;

public class GameTimingController : MonoBehaviour
{
    [SerializeField] private GameTimingEvent[] secondsTilNextEvent;
    [SerializeField] private CurrentGameState gameState;

    private int _eventIndex;
    private float _t;

    private void Start()
    {
        Message.Publish(new UpdateProgressionBar { Description = secondsTilNextEvent[_eventIndex].Description, Progress = 1 });
    }

    private void Update()
    {
        if (_eventIndex == secondsTilNextEvent.Length && !gameState.State.IsSpawning)
            return;
        _t = gameState.State.IsWaterAboveCertainLevel(0.8f) ? _t + Time.deltaTime * 10 : _t + Time.deltaTime;
        if (_t >= secondsTilNextEvent[_eventIndex].SecondsToAppear)
        {
            gameState.UpdateState(x => x.IsSpawning = true);
            secondsTilNextEvent[_eventIndex].Plant.gameObject.SetActive(true);
            _t = 0;
            _eventIndex++;
        }
        Message.Publish(new UpdateProgressionBar { Description = secondsTilNextEvent[_eventIndex].Description, Progress = 1 - _t / secondsTilNextEvent[_eventIndex].SecondsToAppear });
    }
}