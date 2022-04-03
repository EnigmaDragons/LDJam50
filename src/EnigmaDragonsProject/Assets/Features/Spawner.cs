using UnityEngine;

public class Spawner : OnMessage<SpawnNextSegment>
{
    [SerializeField] private GameObject[] objectsToSpawn;
    [SerializeField] private CurrentGameState gameState;

    private int _index;
    
    protected override void Execute(SpawnNextSegment msg)
    {
        if (_index == objectsToSpawn.Length)
            return;
        objectsToSpawn[_index].SetActive(true);
        _index++;
    }
}