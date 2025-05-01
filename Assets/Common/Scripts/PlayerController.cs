using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InteractionDetector interactionDetector;
    private InputAction move;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb;


    private void Awake()
    {
        // Get component references
        rb = GetComponent<Rigidbody2D>();
    }

    // Subscribe to input events
    private void OnEnable()
    {
        move = InputManager.Instance.inputActions.Player.Move;
        InputManager.Instance.inputActions.Player.Interact.performed += DoInteract;
    }

    // Unsubscribe from input events
    private void OnDisable()
    {
        InputManager.Instance.inputActions.Player.Interact.performed -= DoInteract;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = move.ReadValue<Vector2>() * moveSpeed;
    }

    private void DoInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interact");
        interactionDetector.OnInteract();
    }
}
