using System;
using UnityEngine;
using UnityEngine.UI;

public class LightTreeOnFireIfItsNotWatered : MonoBehaviour
{
    [SerializeField] private float timeToImplosion;
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private PlantController plant;
    [SerializeField] private Image image;

    private float _lastWater;
    private float _t;

    private void Start() => _t = timeToImplosion;
    
    private void Update()
    {
        if (_lastWater < gameState.State.PlantById(plant.Id).Water)
            _t = timeToImplosion;
        else if (_t != 0)
        {
            _t = Math.Max(0, _t -= Time.deltaTime);
            if (_t == 0)
                gameState.UpdateState(x => x.PlantById(plant.Id).IsOnFire = true);
        }
        image.fillAmount = gameState.State.PlantById(plant.Id).IsOnFire ? 0 : _t / timeToImplosion;
        _lastWater = gameState.State.PlantById(plant.Id).Water;
    }
}