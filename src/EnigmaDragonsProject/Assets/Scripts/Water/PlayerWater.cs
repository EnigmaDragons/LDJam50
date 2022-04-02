using System;
using System.Collections;
using System.Collections.Generic;
using Codice.CM.Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerWater : MonoBehaviour
{
    [SerializeField] private float maxDistanceFromPump;
    [SerializeField] private float pumpingDelay;
    [SerializeField] private float meleeToolRange;
    [SerializeField] private PlayerTools playerTools;

    private float lastPumpTime;
    private bool isPissing = false;
    private Collider nearestPlant;
    private Collider nearestPump;

    private void Awake()
    {
        playerTools.Reset();
    }

    public void TryTakeWater()
    {
        if (!nearestPump) return;
        if (!(Time.time - lastPumpTime > pumpingDelay)) return;
        lastPumpTime = Time.time;
        playerTools.FillTolls();
    }

    public void TogglePiss(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<float>();
        isPissing = value > 0.5f;
    }

    private void FixedUpdate()
    {
        CheckForPlants();
        CheckForPumps();
        
        if (isPissing) TryPiss();
    }

    private void CheckForPumps()
    {
        var range = maxDistanceFromPump;
        var results = new Collider[1];
        var size = Physics.OverlapSphereNonAlloc(transform.position, range, results, layerMask: LayerMask.GetMask("Pumps"));
        if (size == 0) {nearestPump = null; return;}

        nearestPump = results[0];
    }
    
    private void CheckForPlants()
    {
        var range = meleeToolRange;
        var results = new Collider[1];
        var size = Physics.OverlapSphereNonAlloc(transform.position, range, results, layerMask: LayerMask.GetMask("Plants"));
        if (size == 0) {nearestPlant = null; return;}

        nearestPlant = results[0];
    }

    private Collider lastPlant;
    private PlantController cachedPlant;
    public void TryPiss()
    {
        if (!nearestPlant) return;
        if (cachedPlant is null || lastPlant is null || lastPlant != nearestPlant)
        {
            lastPlant = nearestPlant;
            cachedPlant = lastPlant.GetComponent<PlantController>();
        }
        
        var tool = playerTools.GetMeleeTool();
        var amount = tool.waterTransferRate * Time.deltaTime;
        amount = Math.Max(tool.WaterAmount, amount);
        amount = cachedPlant.AddWater(amount);
        tool.UseWater(amount);
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



