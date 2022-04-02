using KinematicCharacterController.Walkthrough.BasicMovement;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public MyCharacterController Character;
    
    [SerializeField] private DefaultInputActions.PlayerActions _playerActions;
    
    public void Move(InputAction.CallbackContext context)
    {
        PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

        var move = context.action.ReadValue<Vector2>();
        characterInputs.MoveAxisForward = move.y;
        characterInputs.MoveAxisRight = move.x;
        
        Character.SetInputs(ref characterInputs);
    }
}
