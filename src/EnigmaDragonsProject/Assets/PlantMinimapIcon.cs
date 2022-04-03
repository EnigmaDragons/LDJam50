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
    private PlantState state;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        state = gameState.State.PlantById(plant.Id);
    }


    private void Update()
    {
        if (state.Water <= 0)
        {
            renderer.sprite = plantDying;
            return;
        }

        if (state.IsOnFire)
        {
            renderer.sprite = plantOnFire;
            return;
        }

        if (state.Water < state.WaterCapacity * 0.5)
        {
            renderer.sprite = plantNeedWater;
            return;
        }

        renderer.sprite = plantDefault;
    }
    
    
    
}
