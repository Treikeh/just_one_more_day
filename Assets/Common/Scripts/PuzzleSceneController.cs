using System;
using UnityEngine;

public class PuzzleSceneController : MonoBehaviour
{
    private Transform dragTarget = null;
    private Rigidbody2D rb = null;
    private Vector3 offset;


    private void OnEnable()
    {
        InputManager.Instance.onLeftClickPressed += LeftClickPressed;
        InputManager.Instance.onRightClickPressed += RightClickPressed;
    }

    private void OnDisable()
    {
        InputManager.Instance.onLeftClickPressed -= LeftClickPressed;
        InputManager.Instance.onRightClickPressed -= RightClickPressed;
    }

    private void Start()
    {
        // Enable Puzzle InputActionMap
        InputManager.Instance.ChangeActionMap("Puzzle");
    }

    private void Update()
    {
        if (dragTarget)
        {
            dragTarget.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            if (rb && rb.bodyType == RigidbodyType2D.Dynamic)
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
            if (hit)
            {
                dragTarget = hit.transform;
                offset = dragTarget.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                rb = dragTarget.GetComponent<Rigidbody2D>();
            }
        }
        // Drop target
        else if (!pressed && dragTarget)
        {
            rb = null;
            dragTarget = null;
        }
    }

    private void RightClickPressed(bool pressed)
    {
        Debug.Log($"Right click pressed {pressed}");
    }
}
