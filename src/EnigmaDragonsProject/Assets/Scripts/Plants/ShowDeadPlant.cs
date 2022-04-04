using UnityEngine;

public class ShowDeadPlant : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private GameObject plantParent;

    private void Awake()
    {
        foreach (Transform child in plantParent.transform)
            Destroy(child.gameObject);
        var plant = gameState.State.PlantWhoDied;
        if (plant == null || plant.Prefab == null)
            return;

        var p = Instantiate(plant.Prefab, plantParent.transform.position, Quaternion.identity, plantParent.transform);
        foreach (Transform child in p.transform)
        {
            if (child.name == "PlantUI")
                child.gameObject.SetActive(false);
            if (child.gameObject.layer == 8)
                child.gameObject.SetActive(false);
        }
    }
}
