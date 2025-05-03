using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Input")]
    private InputAction move;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb;


    private void Awake()
    {
        // Get component references
        rb = GetComponent<Rigidbody2D>();
        // Set movement 
        move = InputManager.Instance.inputActions.Player.Move;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = move.ReadValue<Vector2>() * moveSpeed;
    }
}
