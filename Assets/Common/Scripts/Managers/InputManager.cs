using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
// CREDITS: One Wheel Studio - Youtube: https://www.youtube.com/watch?v=T8fG0D2_V5M

// Once again i don't like using manager classes especially not MonoBehaviours, but it works and that's the most importat part.

public class InputManager : MonoBehaviour
{
    // Create singleton instance
    public static InputManager Instance {get; private set;}
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
        inputActions = new TopDownInputActions();
    }


    public TopDownInputActions inputActions;
    public UnityAction<InputActionMap> actionMapChange;


    private void Start()
    {
        ToggleActionMap(inputActions.Player);
    }

    public void ToggleActionMap(InputActionMap actionMap)
    {
        if (actionMap.enabled)
            return;
        
        inputActions.Disable();
        actionMapChange?.Invoke(actionMap);
        actionMap.Enable();
    }
}
