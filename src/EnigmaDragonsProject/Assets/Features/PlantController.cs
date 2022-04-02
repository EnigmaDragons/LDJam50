using UnityEngine;

public class PlantController : MonoBehaviour
{
    [SerializeField] private Plant plant;
    [SerializeField] private float currentWater;

    private float _t;
    
    private void Update()
    {
        _t += Time.deltaTime;
        if (_t > 1)
        {
            _t -= 1;
            currentWater -= plant.WaterConsumption;
        }
    }
}