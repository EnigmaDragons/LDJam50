using System;
using System.Collections;
using System.Collections.Generic;
using Codice.CM.Common;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerTools))]
public class PlayerWater : MonoBehaviour
{
    [SerializeField] private float maxDistanceFromPump;
    [SerializeField] private Transform pump;
    [SerializeField] private float pumpingDelay;
    [SerializeField] private float meleeToolRange;
    
    private float lastPumpTime;
    private PlayerTools playerTools;
    private bool isPissing = false;
    private void Awake()
    {
        playerTools = GetComponent<PlayerTools>();        
    }

    private void TakeWater()
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

    public void TogglePiss(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<float>();
        isPissing = value > 0.5f;
    }

    private void FixedUpdate()
    {
        if (isPissing) TryPiss();
    }

    public void TryPiss()
    {
        var range = meleeToolRange;
        var tool = playerTools.GetMeleeTool();
        var results = new Collider[1];
        var size = Physics.OverlapSphereNonAlloc(transform.position, range, results, layerMask: LayerMask.GetMask("Plants"));
        if (size == 0) return;
        
        Collider nearest = null;
        var dis = 10000f;
        foreach (var result in results)
        {
            var newDis = Vector3.SqrMagnitude(result.transform.position - transform.position);
            if (newDis < dis)
            {
                dis = newDis;
                nearest = result;
            }
        }

        var plant = nearest.gameObject.GetComponent<PlantController>();
        
        var amount = tool.waterTransferRate * Time.deltaTime;
        amount = tool.UseWater(amount);
        plant.AddWater(amount);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxDistanceFromPump);
        Gizmos.color = Color.clear;
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, meleeToolRange);
        Gizmos.color = Color.clear;
    }
}

public interface IWaterHolder
{
    public float WaterAmount {get;}
    public float MaxWaterAmount {get;}
    public void Fill();
}



