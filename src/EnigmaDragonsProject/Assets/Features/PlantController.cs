using System;
using System.Linq;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class PlantController : MonoBehaviour
{
    [SerializeField] private Plant plant;
    [SerializeField] private Navigator navigator;
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private Image waterFill;
    [SerializeField] private Image wiltingFill;
    [SerializeField] private AudioSource plantSoundSource;
    [SerializeField] private GameObject fireVFX;
    public Plant Plant => plant;
    
    private const float spreadRange = 10;
    private const float spreadTime = 10;
    private float timeUntilSpread;

    [ShowInInspector] [ReadOnly] private int _id;
    public int Id => _id;

    private void Start()
    {
        gameState.UpdateState(x => _id = x.InitPlant(transform,  plant.StartingWater, plant.WiltingSeconds, plant.WaterCapacity));
        if (waterFill == null)
            Log.Error($"{name} does not have a Water Fill UI");
    }
    
    private void OnEnable() {
        Message.Publish(new PlaySoundRequested(GameSounds.NewPlant, Camera.main.transform.position));
    }

    private void Update()
    {
        var plantState = gameState.State.PlantById(_id);
        var water = plantState.Water;
        var wiltingSecondsRemaining = plantState.WiltingRemainingSeconds;
        var isOnFire = plantState.IsOnFire;

        CheckIfFull(plantState);
            
        if (plantState.Water > 0)
        {
            var waterConsumption = plant.WaterConsumption(water);
            waterConsumption = isOnFire ? waterConsumption + 2.5f : waterConsumption;
            water -= Time.deltaTime * waterConsumption;
            if (water < 0)
            {
                wiltingSecondsRemaining += water / waterConsumption;
                if (wiltingFill != null)
                {

                    wiltingFill.fillAmount = wiltingSecondsRemaining / plant.WiltingSeconds;
                    wiltingFill.color = new Color(1, 1, 1, 1);
                }
                water = 0;
            }
            if (waterFill != null)
                waterFill.fillAmount = water / plantState.WaterCapacity;
        }
        else
        {
            wiltingSecondsRemaining -= Time.deltaTime;
            if (wiltingFill != null)
            {
                wiltingFill.fillAmount = wiltingSecondsRemaining / plant.WiltingSeconds;  
                wiltingFill.color = new Color(1, 1, 1, 1);
            }
        }

        fireVFX.SetActive(isOnFire);
        if (isOnFire)
        {
            Message.Publish(new LoopSoundRequested(GameSounds.TreeFire, plantSoundSource));
            timeUntilSpread -= Time.deltaTime;
            if (timeUntilSpread <= 0)
            {
                SpreadFire();
            }
        }
        else
        {
            timeUntilSpread = spreadTime;
        }

        gameState.UpdateState(x =>
        {
            plantState.Water = water;
            plantState.WiltingRemainingSeconds = wiltingSecondsRemaining;
        });

        if (wiltingSecondsRemaining <= 0 && !gameState.State.Lost)
        {
            gameState.UpdateState(x => x.Lost = true);
            navigator.NavigateToGameOverScene();   
        }
    }

    public async void CheckIfFull(PlantState plantState)
    {
        if (plantState.Water < plant.WaterCapacity) return;
        await Task.Delay(500);
        Message.Publish(new PlaySoundRequested(GameSounds.PlantFull, gameObject.transform.position));     
    }

    public float AddWater(float amount)
    {
        float waterConsumed = 0;
        gameState.UpdateState(x =>
        {
            var plantState = x.PlantById(_id);
            waterConsumed = Math.Min(plant.WaterCapacity - plantState.Water, amount);
            plantState.Water += waterConsumed*gameState.State.playerStats.wateringSpeed;
            if (plantState.Water > 0 && wiltingFill != null)
                wiltingFill.color = new Color(1, 1, 1, 0.5f);
        });
        return waterConsumed;
    }

    private void SpreadFire()
    {
        var results = Physics.OverlapSphere(transform.position, spreadRange, layerMask: LayerMask.GetMask("Plants"));

        var res = results.Where(x => {
            var controller = x.GetComponent<PlantController>();
            return !gameState.State.PlantById(controller.Id).IsOnFire;
        }).FirstOrDefault();

        if (res != null)
        {
            gameState.UpdateState(x => x.PlantById(res.GetComponent<PlantController>().Id).IsOnFire = true);
            timeUntilSpread = spreadTime;
        }
    }
}