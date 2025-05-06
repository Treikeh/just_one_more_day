using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
// CREDITS: One Wheel Studio - Youtube: https://www.youtube.com/watch?v=T8fG0D2_V5M

// Once again i don't like using manager classes especially not MonoBehaviours, but it works and that's the most importat part.

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;


    private void OnEnable()
    {
        GameEventManager.Instance.inputEvents.onActionMapChanged += ActionMapChanged;
    }

    private void OnDisable()
    {
        GameEventManager.Instance.inputEvents.onActionMapChanged -= ActionMapChanged;
    }


    private void ActionMapChanged(string actionMap)
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.SwitchCurrentActionMap(actionMap);
    }


    // PLAYER INPUTS
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            GameEventManager.Instance.inputEvents.MovePressed(context.ReadValue<Vector2>());
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventManager.Instance.inputEvents.InteractPressed();
        }
    }

    public void OnJournal(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventManager.Instance.inputEvents.JournalPressed();
        }
    }


    // UI INPUTS
    public void OnAdvance(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventManager.Instance.inputEvents.AdvancePressed();
        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventManager.Instance.inputEvents.CancelPressed();
        }
    }


    // PUZZLE INPUTS
    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventManager.Instance.inputEvents.LeftClickPressed(true);
        }
        else if (context.canceled)
        {
            GameEventManager.Instance.inputEvents.LeftClickPressed(false);
        }
    }

    public void OnRightClickPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameEventManager.Instance.inputEvents.RightClickPressed(true);
        }
        else if (context.canceled)
        {
            GameEventManager.Instance.inputEvents.RightClickPressed(false);
        }
    }
}
