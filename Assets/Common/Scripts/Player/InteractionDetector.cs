using UnityEngine;
using UnityEngine.InputSystem;
// CREDITS: Game Code Library - Youtube: https://www.youtube.com/watch?v=MPP9GLp44Pc

public class InteractionDetector : MonoBehaviour
{
    [SerializeField] private GameObject interactIcon;
    private IInteractable interactableInRange = null; // Closeset Interactable


    // Subscribe and unsubscribe from input events
    private void OnEnable() { InputManager.Instance.inputActions.Player.Interact.started += OnInteract; }
    private void OnDisable() { InputManager.Instance.inputActions.Player.Interact.started -= OnInteract; }


    private void Start()
    {
        interactIcon.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactIcon.SetActive(false);
        }
    }


    public void OnInteract(InputAction.CallbackContext context)
    {
        interactableInRange?.Interact();
    }
}
