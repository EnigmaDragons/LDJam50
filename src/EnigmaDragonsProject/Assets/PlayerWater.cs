using System.Collections;
using System.Collections.Generic;
using Codice.CM.Common;
using UnityEngine;

public class PlayerWater : MonoBehaviour
{
    [SerializeField] private float maxWater;
    [SerializeField] private WaterPump testPump;
    //TODO get all water holders from inventory
    private List<IWaterHolder> waterHolders;
    
    public void TakeWater(WaterPump pump)
    {
        foreach (IWaterHolder holder in waterHolders)
        {
            holder.WaterAmount += pump.TakeWater();
        }
    }


    
    
    public void TryTakeWater()
    {
                           
    }
    
    
}

public interface IWaterHolder
{
    public float WaterAmount {get; set; }
}



