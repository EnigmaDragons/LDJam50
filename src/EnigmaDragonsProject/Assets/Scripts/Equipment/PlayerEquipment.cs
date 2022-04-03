using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject waterBottle;

    public void EquipWaterBottle()
    {
        UnequipRightHand();
        waterBottle.gameObject.SetActive(true);
    }

    private void UnequipRightHand()
    {
        foreach (Transform child in rightHand.transform)
            child.gameObject.SetActive(false);
    }
}
