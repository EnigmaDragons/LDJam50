using System;
using System.Collections;
using System.Collections.Generic;
using Codice.CM.Common;
using UnityEngine;

[RequireComponent(typeof(PlayerTools))]
public class PlayerWater : MonoBehaviour
{
    [SerializeField] private float maxDistanceFromPump;
    [SerializeField] private Transform pump;
    [SerializeField] private float pumpingDelay;
    private float lastPumpTime;
    private PlayerTools playerTools;
    private void Awake()
    {
        playerTools = GetComponent<PlayerTools>();        
    }

    public void TakeWater()
    {
        playerTools.FillTolls();
    }

    
    public void TryTakeWater()
    {
        if (!(Vector3.SqrMagnitude(pump.transform.position - transform.position) <
              maxDistanceFromPump * maxDistanceFromPump)) return;

        if (!(Time.time - lastPumpTime > pumpingDelay)) return;
        
        lastPumpTime = Time.time;
        TakeWater();
    }
    
    
}

public interface IWaterHolder
{
    public float WaterAmount {get;}
    public float MaxWaterAmount {get;}
    public void Fill();
}



