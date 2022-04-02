using UnityEngine;

public class GameTimingController : MonoBehaviour
{
    [SerializeField] private GameTimingEvent[] events;

    private int _eventIndex;
    private float _t;

    private void Update()
    {
        if (_eventIndex == events.Length)
            return;
        _t += Time.deltaTime;
        if (_t >= events[_eventIndex].SecondsToAppear)
        {
            events[_eventIndex].Tile.gameObject.SetActive(true);
            _t -= events[_eventIndex].SecondsToAppear;
            _eventIndex++;
        }
    }
}