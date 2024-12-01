using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {

    private PlayerInputActions playerInputActions;
    [SerializeField] private bool isInputEnabled;

    public void EnableInput(bool enable)
    {
        isInputEnabled = enable;
    }

    private void Awake() {
        // Initialize PlayerInputActions and enable the Player action map
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        if (!isInputEnabled)
        {
            return Vector2.zero;
        }

        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }

    public bool IsPausePressed()
    {
        if (!isInputEnabled)
        {
            return false;
        }
        return playerInputActions.Player.Pause.WasPressedThisFrame();
    }

    public bool IsInteractPressed()
    {
        if (!isInputEnabled)
        {
            return false;
        }
        return playerInputActions.Player.Interact.triggered;
    }

}
