using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject waterBottle;
    [SerializeField] private GameObject waterSprayBottle;
    [SerializeField] private GameObject wateringCan;

    public void EquipWaterBottle()
    {
        UnequipRightHand();
        waterBottle.gameObject.SetActive(true);
    }

    public void EquipWaterSprayBottle()
    {
        UnequipRightHand();
        waterSprayBottle.gameObject.SetActive(true);
    }

    public void EquipWateringCan()
    {
        UnequipRightHand();
        wateringCan.gameObject.SetActive(true);
    }
    
    private void UnequipRightHand()
    {
        foreach (Transform child in rightHand.transform)
            child.gameObject.SetActive(false);
    }
}
