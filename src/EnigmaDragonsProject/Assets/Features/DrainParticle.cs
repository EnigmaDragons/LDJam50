using System;
using UnityEngine;

public class DrainParticle : MonoBehaviour
{
    [SerializeField] private float travelTime = 1;

    private Vector3 _start;
    private Vector3 _destination;
    private float _travelTime;
    
    public void Init(Vector3 start, Vector3 destination)
    {
        _start = start;
        _destination = destination;
        _travelTime = travelTime;
    }

    private void Update()
    {
        if (_travelTime == 0)
            Destroy(gameObject);
        _travelTime = Math.Max(0, _travelTime - Time.deltaTime);
        var position = Vector3.Lerp(_start, _destination, 1 - (_travelTime / travelTime));
        position.y += (0.5f - Math.Abs(0.5f - _travelTime / travelTime)) * 5f;
        transform.position = position;
    }
}