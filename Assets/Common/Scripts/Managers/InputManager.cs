using System;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public event Action<Vector2> OnMovePressed;
    public event Action OnInteractPressed;
    public event Action OnJournalPressed;
    public event Action OnAdvancePressed;
    public event Action<bool> OnLeftClickPressed;
    public event Action<bool> OnRightClickPressed;

    public static InputManager Instance { get; private set; }

    private PlayerInput playerInput;


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


    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            OnMovePressed?.Invoke(context.ReadValue<Vector2>());
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnInteractPressed?.Invoke();
        }
    }

    public void OnJournal(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnJournalPressed?.Invoke();
        }
    }


    // UI INPUTS
    public void OnAdvance(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnAdvancePressed?.Invoke();
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
    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnLeftClickPressed?.Invoke(true);
        }
        else if (context.canceled)
        {
            OnLeftClickPressed?.Invoke(false);
        }
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnRightClickPressed?.Invoke(true);
        }
        else if (context.canceled)
        {
            OnRightClickPressed?.Invoke(true);
        }
    }
}
