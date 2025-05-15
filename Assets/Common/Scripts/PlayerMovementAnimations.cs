using System;
using UnityEngine;

public class PlayerMovementAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private Vector2 moveInput;


    private void OnEnable() { InputManager.Instance.OnMovePressed += MovePressed; }
    private void OnDisable() { InputManager.Instance.OnMovePressed -= MovePressed; }



    private void Update()
    {
        if (moveInput.magnitude > 0.1f || moveInput.magnitude < -0.1f)
        {
            animator.SetBool("isMoving", true);
            animator.SetFloat("x", moveInput.x);
            animator.SetFloat("y", moveInput.y);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }


    private void MovePressed(Vector2 vector)
    {
        moveInput = vector;
    }
}
