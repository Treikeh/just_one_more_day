using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
// CREDITS: One Wheel Studio - Youtube: https://www.youtube.com/watch?v=T8fG0D2_V5M

// Once again i don't like using manager classes especially not MonoBehaviours, but it works and that's the most importat part.

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    private PlayerInput playerInput;


    // Set singleton instance
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        playerInput = GetComponent<PlayerInput>();
    }


    public void ChangeActionMap(string actionMap)
    {
        playerInput.SwitchCurrentActionMap(actionMap);
    }


    // PLAYER INPUTS
    public event Action<Vector2> onMovePressed;
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            onMovePressed?.Invoke(context.ReadValue<Vector2>());
        }
    }

    public event Action onInteractPressed;
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onInteractPressed?.Invoke();
        }
    }

    public event Action onJournalPressed;
    public void OnJournal(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onJournalPressed?.Invoke();
        }
    }


    // UI INPUTS
    public event Action onAdvancePressed;
    public void OnAdvance(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onAdvancePressed?.Invoke();
        }
    }

    public event Action onCancelPressed;
    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onCancelPressed?.Invoke();
        }
    }


    // PUZZLE INPUTS
    public event Action<bool> onLeftClickPressed;
    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onLeftClickPressed?.Invoke(true);
        }
        else if (context.canceled)
        {
            onLeftClickPressed?.Invoke(false);
        }
    }

    public event Action<bool> onRightClickPressed;
    public void OnRightClickPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onRightClickPressed?.Invoke(true);
        }
        else if (context.canceled)
        {
            onRightClickPressed?.Invoke(true);
        }
    }
}
