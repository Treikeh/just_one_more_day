using System;
using UnityEngine;


public class PuzzleSceneController : MonoBehaviour
{
    private const float hoverScale = 1.2f;

    private bool lmbPressed = false;
    private Vector3 offset;
    private Rigidbody2D rb = null;
    private GameObject hoverObject = null;


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
        if (rb && lmbPressed)
        {
            rb.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            if (rb.bodyType == RigidbodyType2D.Dynamic)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }

        // Hover effect
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit && hit.collider.GetComponent<Rigidbody2D>() && !hoverObject)
        {
            hoverObject = hit.collider.gameObject;
            hoverObject.transform.localScale = new Vector3(hoverScale, hoverScale, 1f);
        }
        else if (!hit && hoverObject && !lmbPressed)
        {
            hoverObject.transform.localScale = new Vector3(1f, 1f, 1f);
            hoverObject = null;
        }
    }


    private void LeftClickPressed(bool pressed)
    {
        lmbPressed = pressed;
        // Get rigidbody of hoverObject when Cliking Left Mouse Button
        if (lmbPressed && hoverObject)
        {
            rb = hoverObject.GetComponent<Rigidbody2D>();
            offset = rb.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (!lmbPressed && rb)
        {
            rb = null;
        }
    }


    private void RightClickPressed(bool pressed)
    {
        Debug.Log($"Right click pressed {pressed}");
    }
}
