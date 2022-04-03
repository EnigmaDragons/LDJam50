using UnityEngine;

public class PlayerEquipment : OnMessage<GameStateChanged>
{
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject leftHand;

    [SerializeField] private GameObject waterBottle;
    [SerializeField] private WateringTool waterBottleTool;

    [SerializeField] private GameObject waterSprayBottle;
    [SerializeField] private WateringTool waterSprayBottleTool;

    [SerializeField] private GameObject wateringCan;
    [SerializeField] private WateringTool wateringCanTool;

    [SerializeField] private GameObject bucket;
    [SerializeField] private WateringTool bucketTool;

    [SerializeField] private GameObject waterBalloon;
    [SerializeField] private WateringTool waterBalloonTool;

    [SerializeField] private GameObject waterGun1;
    [SerializeField] private WateringTool waterGun1Tool;

    private void Start()
    {
        UnequipLeftHand();
        UnequipRightHand();
        EquipWaterBottle();
        EquipWaterGun1();
    }
    
    public void EquipWaterBalloon()
    {
        UnequipLeftHand();
        waterBalloon.gameObject.SetActive(true);
    }

    public void EquipWaterGun1()
    {
        UnequipLeftHand();
        waterGun1.gameObject.SetActive(true);
    }
    
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

    public void EquipBucket()
    {
        UnequipRightHand();
        bucket.gameObject.SetActive(true);
    }
    
    private void UnequipRightHand()
    {
        foreach (Transform child in rightHand.transform)
            child.gameObject.SetActive(false);
    }

    private void UnequipLeftHand()
    {
        foreach (Transform child in leftHand.transform)
            child.gameObject.SetActive(false);
    }

    protected override void Execute(GameStateChanged gameStateChanged)
    {
        var gameState = gameStateChanged.State;
        
        if (gameState.MeleeTool == waterBottleTool) EquipWaterBottle();
        if (gameState.MeleeTool == bucketTool) EquipBucket();
        if (gameState.MeleeTool == wateringCanTool) EquipWateringCan();

        if (gameState.RangedTool == waterSprayBottleTool) EquipWaterSprayBottle();
        if (gameState.RangedTool == waterBalloonTool) EquipWaterBalloon();
        if (gameState.RangedTool == waterGun1Tool) EquipWaterGun1();
    }
}
