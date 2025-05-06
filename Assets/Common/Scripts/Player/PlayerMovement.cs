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


    private void Awake()
    {
        // Get component references
        rb = GetComponent<Rigidbody2D>();
        // Set movement 
        GameEventManager.Instance.inputEvents.onMovePressed += MovePressed;
    }

    private void OnDestory()
    {
        GameEventManager.Instance.inputEvents.onMovePressed -= MovePressed;
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
