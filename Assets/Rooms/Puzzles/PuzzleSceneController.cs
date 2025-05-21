using System;
using UnityEngine;

public class PuzzleSceneController : MonoBehaviour
{
    private Rigidbody2D rb = null;
    private Vector3 offset;


    private void OnEnable()
    {
        InputManager.Instance.OnLeftClickPressed += LeftClickPressed;
        InputManager.Instance.OnRightClickPressed += RightClickPressed;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnLeftClickPressed -= LeftClickPressed;
        InputManager.Instance.OnRightClickPressed -= RightClickPressed;
    }


    private void Start()
    {
        // Enable Puzzle InputActionMap
        InputManager.Instance.ChangeActionMap("Puzzle");
        UiManager.Instance.HideInGameUi();
    }


    private void Update()
    {
        if (rb)
        {
            rb.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            if (rb.bodyType == RigidbodyType2D.Dynamic)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }


    private void LeftClickPressed(bool pressed)
    {
        Debug.Log($"Left click pressed: {pressed}");
        if (pressed)
        {
            // Pick up target
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit && hit.collider.TryGetComponent(out Rigidbody2D rigidbody))
            {
                rb = rigidbody;
                offset = rb.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        // Drop target
        else if (!pressed && rb)
        {
            rb = null;
        }
    }


    private void RightClickPressed(bool pressed)
    {
        Debug.Log($"Right click pressed {pressed}");
    }
}
