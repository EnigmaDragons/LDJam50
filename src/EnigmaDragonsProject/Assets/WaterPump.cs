using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPump : MonoBehaviour
{
    [SerializeField] private float refillRete;


    public float TakeWater()
    {
        return refillRete * Time.deltaTime;
    }
}
