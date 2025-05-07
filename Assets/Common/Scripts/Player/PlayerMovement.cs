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
    private Vector2 velocity;
    private Rigidbody2D rb;


    private void OnEnable() { InputManager.Instance.onMovePressed += MovePressed; }
    private void OnDisable() { InputManager.Instance.onMovePressed -= MovePressed; }


    private void Start()
    {
        // Get component references
        rb = GetComponent<Rigidbody2D>();
        InputManager.Instance.ChangeActionMap("Player");
    }


    private void MovePressed(Vector2 moveDir)
    {
        velocity = moveDir * moveSpeed;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = velocity;
    }
}
