using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{

    public Vector2 MovementValue { get; private set; }
    public Vector2 PositionValue { get; private set; }
    public bool IsRunning { get; private set; }
    public bool Jump { get; set; }
    public bool Fire { get; set; }

    private Controls controls;

    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Jump = true;
        }
        else if (context.canceled)
        {
            Jump = false;
        }
    }

    public void OnPosition(InputAction.CallbackContext context)
    {
        PositionValue = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Fire = true;
        }
        else if (context.canceled)
        {
            Fire = false;
        }
    }
}
