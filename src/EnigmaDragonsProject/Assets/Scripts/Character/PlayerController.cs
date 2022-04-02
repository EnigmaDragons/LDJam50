using KinematicCharacterController.Examples;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public ExampleCharacterController Character;
    [SerializeField] private Animator animator;
    
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
