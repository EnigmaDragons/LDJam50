using KinematicCharacterController.Examples;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public ExampleCharacterController Character;
    [SerializeField] private GameObject character;
    
    [SerializeField] private DefaultInputActions.PlayerActions _playerActions;
    private Quaternion _lookDirection;

    public void FixedUpdate()
    {
        Character.UpdateRotation(ref _lookDirection, Time.deltaTime);
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        var characterInputs = new PlayerCharacterInputs();

        var move = context.action.ReadValue<Vector2>();
        characterInputs.MoveAxisForward = move.y;
        characterInputs.MoveAxisRight = move.x;
        
        Character.SetInputs(ref characterInputs);
        Debug.Log($"Move {move}");
        //
        // var movementDirection = new Vector3(move.x, 0, move.y);
        // movementDirection.Normalize();
        //
        // var rotation = Quaternion.LookRotation(character.transform.position + movementDirection, Vector3.up);
        // if (rotation.x == 0 && rotation.y == 0 && rotation.z == 0 && rotation.w == 0)
        //     return;
        // else
        //     _lookDirection = rotation;
        // Debug.Log(rotation);
    }
}
