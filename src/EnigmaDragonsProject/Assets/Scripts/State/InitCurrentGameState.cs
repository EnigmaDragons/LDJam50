using System.Collections;
using UnityEngine;

public sealed class InitCurrentGameState : MonoBehaviour
{
    [SerializeField] private CurrentGameState state;

    void Awake() => state.Init();

    private void Start()
    {
        StartCoroutine(MarkPlantSpawningComplete());
    }

    private IEnumerator MarkPlantSpawningComplete()
    {
        yield return new WaitForSeconds(1f);
        state.UpdateState(s => s.PlantSpawningComplete = true);
    }
}
