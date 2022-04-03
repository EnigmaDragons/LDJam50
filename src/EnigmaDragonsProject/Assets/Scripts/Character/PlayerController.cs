using KinematicCharacterController.Examples;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ExampleCharacterController Character;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource playerSoundSource;
    
    private bool _isMoving;

    private void Update()
    {
        var characterInputs = new PlayerCharacterInputs();
        var move = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        characterInputs.MoveAxisForward = move.x;
        characterInputs.MoveAxisRight = move.y;
        Character.SetInputs(ref characterInputs);

        var isMoving = move.magnitude > 0;
        UpdateIsMoving(isMoving);
    }

    private void UpdateIsMoving(bool isMoving)
    {
        if (_isMoving == isMoving)
            return;
        if (isMoving) 
        {
            Message.Publish(new LoopSoundRequested(GameSounds.FootStep, playerSoundSource));
        }
        else
        {
            Message.Publish(new StopSoundRequested(GameSounds.FootStep, playerSoundSource));
        }
        _isMoving = isMoving;
        animator.SetBool("IsWalking", isMoving);
    }
}
