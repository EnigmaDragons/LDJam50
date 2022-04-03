using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimingController : MonoBehaviour
{
    [SerializeField] private GameTimingEvent[] secondsTilNextEvent;
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;
    
    private int _eventIndex;
    private float _t;

    private void Start()
    {
        text.text = secondsTilNextEvent[_eventIndex].Description;
    }

    private void Update()
    {
        if (_eventIndex == secondsTilNextEvent.Length && !gameState.State.IsSpawning)
            return;
        _t = gameState.State.IsWaterAboveCertainLevel(0.8f) ? _t + Time.deltaTime * 10 : _t + Time.deltaTime;
        if (_t >= secondsTilNextEvent[_eventIndex].SecondsToAppear)
        {
            gameState.UpdateState(x => x.IsSpawning = true);
            Message.Publish(new SpawnNextSegment());
            _t = 0;
            _eventIndex++;
            text.text = secondsTilNextEvent[_eventIndex].Description;
        }
        image.fillAmount = _t / secondsTilNextEvent[_eventIndex].SecondsToAppear;
    }
}