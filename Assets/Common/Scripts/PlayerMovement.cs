using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    private Vector2 velocity;
    private Rigidbody2D rb;


    private void OnEnable() { InputManager.Instance.OnMovePressed += MovePressed; }
    private void OnDisable() { InputManager.Instance.OnMovePressed -= MovePressed; }


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
