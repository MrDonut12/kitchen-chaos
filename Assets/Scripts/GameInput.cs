using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private InputSystem_Actions playerInputAction;
    public event EventHandler OneInteractionEvent;
  
    
    private void Awake()
    {
        
        playerInputAction = new InputSystem_Actions();
        playerInputAction.Player.Enable();

        playerInputAction.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OneInteractionEvent?.Invoke(obj, EventArgs.Empty);
    }

    public Vector3 getNormalizedVector()
    {
        Vector2 InputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        Vector3 MoveVector = new Vector3(InputVector.x, 0f, InputVector.y);
        MoveVector = MoveVector.normalized;
        return MoveVector;
    }

    public bool IsJumpPressed()
    {
        return playerInputAction.Player.Jump.WasPressedThisFrame();
    }
}
