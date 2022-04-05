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
    [Space]
    [SerializeField] private GameObject waterGun1;
    [SerializeField] private WateringTool waterGun1Tool;

    [Space]
    [SerializeField] private GameObject waterGun2;
    [SerializeField] private WateringTool waterGun2Tool;
    
    [Space]
    [SerializeField] private GameObject waterGun3;
    [SerializeField] private WateringTool waterGun3Tool;
    
    
    private void Start()
    {
        UnequipLeftHand();
        UnequipRightHand();
        EquipWaterBottle();
    }
    
    public void Equip(GameObject prefab, bool leftHand = false){
        if(!leftHand) UnequipRightHand();
        else UnequipLeftHand();
        
        prefab.gameObject.SetActive(true);
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
        
        //Melee
        if (gameState.MeleeTool == waterBottleTool) EquipWaterBottle();
        if (gameState.MeleeTool == bucketTool) EquipBucket();
        if (gameState.MeleeTool == wateringCanTool) EquipWateringCan();
        if (gameState.MeleeTool == waterSprayBottleTool) EquipWaterSprayBottle();
       
        
        //Ranged
        if (gameState.RangedTool == waterBalloonTool) EquipWaterBalloon();
        if (gameState.RangedTool == waterGun1Tool) EquipWaterGun1();
        if (gameState.RangedTool == waterGun2Tool) Equip(waterGun2, true);
        if (gameState.RangedTool == waterGun3Tool) Equip(waterGun3, true);
    }
}
