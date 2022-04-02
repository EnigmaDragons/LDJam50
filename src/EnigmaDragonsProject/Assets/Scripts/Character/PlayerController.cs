using KinematicCharacterController.Examples;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public ExampleCharacterController Character;
    [SerializeField] private Animator animator;
    
    private bool _isMoving;

    public void Move(InputAction.CallbackContext context)
    {
        var characterInputs = new PlayerCharacterInputs();
        var move = context.action.ReadValue<Vector2>();
        characterInputs.MoveAxisForward = move.y;
        characterInputs.MoveAxisRight = move.x;
        Character.SetInputs(ref characterInputs);
        
        var isMoving = move.magnitude > 0;
        UpdateIsMoving(isMoving);
        Debug.Log($"Move {move}");
    }

    private void UpdateIsMoving(bool isMoving)
    {
        if (_isMoving == isMoving)
            return;

        _isMoving = isMoving;
        animator.SetBool("IsWalking", isMoving);
    }
}
