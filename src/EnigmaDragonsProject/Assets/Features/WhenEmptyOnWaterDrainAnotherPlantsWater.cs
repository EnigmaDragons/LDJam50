using System.Linq;
using UnityEngine;

public class WhenEmptyOnWaterDrainAnotherPlantsWater : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private PlantController plant;
    [SerializeField] private DrainParticle particle;
    [SerializeField] private float waterToStealAmount = 10; 

    private void Update()
    {
        var plantState = gameState.State.PlantById(plant.Id);
        if (plantState.Water <= 0 && gameState.State.Plants.Any(x => x.Water > waterToStealAmount))
        {
            var plantToStealFrom = gameState.State.Plants.Where(x => x.Water > waterToStealAmount).Random();
            gameState.UpdateState(x =>
            {
                plantState.Water += waterToStealAmount;
                plantToStealFrom.Water -= waterToStealAmount;
            });
            var inst = Instantiate(particle, plantToStealFrom.Transform.position, Quaternion.identity);
            inst.Init(plantToStealFrom.Transform.position, transform.position);
        }
    }
}