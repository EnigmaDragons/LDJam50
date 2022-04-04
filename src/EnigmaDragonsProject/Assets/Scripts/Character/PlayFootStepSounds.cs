using UnityEngine;

public class PlayFootStepSounds : MonoBehaviour
{
    [SerializeField] private AudioClipVolume[] steps;
    [SerializeField] private float timeBetweenSteps = 0.3f;

    private PlayerController playerController;
    private float cooldownRemaining;
    private bool isWalking;
    private int index;

    private void Start() 
    {
        playerController = GetComponent<PlayerController>();
    }

    private void FixedUpdate() 
    {
        cooldownRemaining = Mathf.Max(0, cooldownRemaining - Time.deltaTime);
        if (!playerController.IsMoving || cooldownRemaining > 0)
            return;
        
        cooldownRemaining = timeBetweenSteps;
        index = (index + 1) % steps.Length;
        var stepSound = steps[index];
        Message.Publish(new PlayOneShot(stepSound, GameObject.FindGameObjectWithTag("Player").transform.position));      
    }
}
