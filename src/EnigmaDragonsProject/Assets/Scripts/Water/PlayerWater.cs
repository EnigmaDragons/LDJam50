using System.Collections;
using System.Collections.Generic;
using Codice.CM.Common;
using UnityEngine;

public class PlayerWater : MonoBehaviour
{
    [SerializeField] private float maxDistanceFromPump;
    [SerializeField] private WaterPump pump;
    [SerializeField] private float pumpingDelay;
    private float lastPumpTime;
    //TODO get all water holders from inventory
    private List<IWaterHolder> waterHolders = new List<IWaterHolder>();
    
    public void TakeWater(WaterPump pump)
    {
        foreach (IWaterHolder holder in waterHolders)
        {
            holder.Fill();
        }
    }

    
    public void TryTakeWater()
    {
        if (!(Vector3.SqrMagnitude(pump.transform.position - transform.position) <
              maxDistanceFromPump * maxDistanceFromPump)) return;

        if (!(Time.time - lastPumpTime > pumpingDelay)) return;
        
        lastPumpTime = Time.time;
        TakeWater(pump);
    }
    
    
}

public interface IWaterHolder
{
    public float WaterAmount {get;}
    public float MaxWaterAmount {get;}
    public void Fill();
}



