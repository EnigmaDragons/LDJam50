using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPump : MonoBehaviour
{
    [SerializeField] private float amountPerClick;


    public float TakeWater()
    {
        return amountPerClick;
    }
}
