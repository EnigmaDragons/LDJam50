using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;

public class PlantMinimapIcon : MonoBehaviour
{
    
    [SerializeField] private CurrentGameState gameState;
    [PreviewField]
    [SerializeField] private Sprite plantDefault;
    [PreviewField]
    [SerializeField] private Sprite plantNeedWater;
    [PreviewField]
    [SerializeField] private Sprite plantOnFire;
    [PreviewField]
    [SerializeField] private Sprite plantDying;
    private SpriteRenderer renderer;

    [SerializeField]
    private PlantController plant;
    private PlantState State => gameState.State.PlantById(plant.Id);
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        if (State.Water <= 0)
            renderer.sprite = plantDying;
        else if (State.IsOnFire)
            renderer.sprite = plantOnFire;
        else if (State.Water < State.WaterCapacity * 0.5)
            renderer.sprite = plantNeedWater;
        else
            renderer.sprite = plantDefault;
    }
}
