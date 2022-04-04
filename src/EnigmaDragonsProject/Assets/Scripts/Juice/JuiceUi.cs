
using UnityEngine;

public class JuiceUi : OnMessage<NewPlantSpawned>
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject newPlantJuiceProto;
    
    protected override void Execute(NewPlantSpawned msg)
    {
        if (gameState.State.PlantSpawningComplete)
            Instantiate(newPlantJuiceProto, Vector3.zero, Quaternion.identity, parent.transform);
    }
}
