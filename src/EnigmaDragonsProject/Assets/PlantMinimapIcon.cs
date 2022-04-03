using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;

public class PlantMinimapIcon : MonoBehaviour
{
    
    [SerializeField] private CurrentGameState gameState;
    
    
    [SerializeField] private Sprite plantDefault;
    [SerializeField] private Sprite plantNeedWater;
    [SerializeField] private Sprite plantOnFire;
    [SerializeField] private Sprite plantDying;
    private SpriteRenderer renderer;

    [SerializeField]
    private PlantController plant;
    [ShowInInspector]
    private PlantState State => gameState.State.PlantById(plant.Id);
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        if (State.Water <= 0)
        {
            renderer.sprite = plantDying;
            return;
        }

        if (State.IsOnFire)
        {
            renderer.sprite = plantOnFire;
            return;
        }

        if (State.Water < State.WaterCapacity * 0.5)
        {
            renderer.sprite = plantNeedWater;
            return;
        }

        renderer.sprite = plantDefault;
    }
    
    
    
}
