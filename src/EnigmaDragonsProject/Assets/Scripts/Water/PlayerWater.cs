using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerWater : MonoBehaviour
{
    [SerializeField] private float maxDistanceFromPump;
    [SerializeField] private float pumpingDelay;
    [SerializeField] private float meleeToolRange;
    [SerializeField] private PlayerTools playerTools;
    [SerializeField] private GameObject waterParticle;
    [SerializeField] private AudioSource wateringSoundSource;
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private WaterShot rangePrototype;

    private float lastPumpTime;
    private bool isPissing = false;
    private Collider nearestPlant;
    private Collider nearestPump;
    private Animator animator;
    private ParticleSystem waterParticles;
    private float _cooldown;
    
    private void Awake()
    {
        playerTools.Reset();
        waterParticles = waterParticle.GetComponent<ParticleSystem>();
        StopWatering();
    }

    public void SetAnimator(Animator a) => animator = a;

    public void TryTakeWater()
    {
        if (!nearestPump) return;
        if (!(Time.time - lastPumpTime > pumpingDelay)) return;
        lastPumpTime = Time.time;
        playerTools.FillTolls();
        
        if (animator != null)
            animator.SetTrigger("FillWater");
        Message.Publish(new PlaySoundRequested(GameSounds.FillWater, nearestPump.transform.position));
    }

    public void TogglePiss(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<float>();
        isPissing = value > 0.5f && playerTools.GetMeleeTool().WaterAmount > 0;
        if (isPissing)
            StartWatering();
        else
            StopWatering();
    }

    public void FireWaterCharge(InputAction.CallbackContext context)
    {
        if (_cooldown > 0)
            return;
        var value = context.ReadValue<float>();
        var rangedTool = playerTools.GetRangedTool();
        if (value > 0.5f && rangedTool != null && rangedTool.WaterAmount > 0)
        {
            rangedTool.UseCharge();
            _cooldown = 0.2f;
            var prototype = Instantiate(rangePrototype, transform.position + new Vector3(0, 1, 0) + transform.forward * 1, Quaternion.identity);
            prototype.Init(rangedTool.waterTransferRate, transform.forward, rangedTool.range);
        }
    }

    private void StartWatering()
    {
        if (!nearestPlant) return;
        waterParticles.Play();
        Message.Publish(new LoopSoundRequested(GameSounds.Watering, wateringSoundSource));
        if (animator != null)
            animator.SetBool("IsPouringWater", true);
    }

    private void StopWatering()
    {
        waterParticles.Stop();
        Message.Publish(new StopSoundRequested(GameSounds.Watering, wateringSoundSource));
        if (animator != null)
            animator.SetBool("IsPouringWater", false);
    }
    
    private void FixedUpdate()
    {
        _cooldown = Math.Max(0, _cooldown - Time.fixedTime);
        CheckForPlants();
        CheckForPumps();

        if (isPissing && playerTools.GetMeleeTool().WaterAmount <= 0)
        {
            isPissing = false;
            StopWatering();
        }

        if (isPissing) TryPiss();
        if (!isPissing)
            StopWatering();
    }

    private void CheckForPumps()
    {
        var range = maxDistanceFromPump;
        var results = new Collider[1];
        var size = Physics.OverlapSphereNonAlloc(transform.position, range, results, layerMask: LayerMask.GetMask("Pumps"));
        if (size == 0) {nearestPump = null; return;}

        nearestPump = results[0];
    }
    
    private void CheckForPlants()
    {
        var range = meleeToolRange;
        var results = new Collider[1];
        var size = Physics.OverlapSphereNonAlloc(transform.position, range, results, layerMask: LayerMask.GetMask("Plants"));
        if (size == 0) {nearestPlant = null; return;}

        nearestPlant = results[0];
    }

    private Collider lastPlant;
    private PlantController cachedPlant;
    public void TryPiss()
    {
        if (!nearestPlant)
        {
            waterParticles.Stop();
            return;
        }
        if (cachedPlant is null || lastPlant is null || lastPlant != nearestPlant)
        {
            lastPlant = nearestPlant;
            cachedPlant = lastPlant.GetComponent<PlantController>();
        }
        
        var tool = playerTools.GetMeleeTool();
        float amount = tool.waterTransferRate * Time.deltaTime;
        amount = Math.Min(tool.WaterAmount, amount);
        amount = cachedPlant.AddWater(amount);
        tool.UseWater(amount);
        waterParticles.Play();
        if (tool.WaterAmount == 0)
        {
            isPissing = false;
            waterParticles.Stop();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxDistanceFromPump);
        Gizmos.color = Color.clear;
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, meleeToolRange);
        Gizmos.color = Color.clear;
    }
}

public interface IWaterHolder
{
    public float WaterAmount {get;}
    public float MaxWaterAmount {get;}
    public void Fill();
}



