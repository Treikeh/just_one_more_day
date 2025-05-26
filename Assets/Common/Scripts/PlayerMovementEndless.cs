using System;
using UnityEngine;


public class PlayerMovementEndless : MonoBehaviour
{
    public enum CharacterMoveDir { UP, DOWN, LEFT, RIGHT, }

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private CharacterMoveDir chrMoveDir = CharacterMoveDir.UP;

    private Vector2 inputDir = Vector2.zero;
    private Vector2 moveDir = Vector2.zero;
    private Rigidbody2D rb;


    private void OnEnable() { InputManager.Instance.OnMovePressed += MovePressed; }
    private void OnDisable() { InputManager.Instance.OnMovePressed -= MovePressed; }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        switch (chrMoveDir)
        {
            case CharacterMoveDir.UP:
                moveDir = Vector2.up + new Vector2(inputDir.x, 0f);
                break;
            case CharacterMoveDir.DOWN:
                moveDir = Vector2.down + new Vector2(inputDir.x, 0f);
                break;
            case CharacterMoveDir.LEFT:
                moveDir = Vector2.left + new Vector2(0f, inputDir.y);
                break;
            case CharacterMoveDir.RIGHT:
                moveDir = Vector2.right + new Vector2(0f, inputDir.y);
                break;
        }

        rb.linearVelocity = moveDir * moveSpeed;
    }


    private void MovePressed(Vector2 vector)
    {
        inputDir = vector;
    }


    public void ChangeMoveDirection(int newChrMovedir)
    {
        chrMoveDir = (CharacterMoveDir)newChrMovedir;
    }
}
