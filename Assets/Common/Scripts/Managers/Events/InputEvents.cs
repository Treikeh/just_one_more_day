using System;
using UnityEngine;

public class InputEvents
{
    // GENERAL
    public event Action<string> onActionMapChanged;
    public void ActionMapChanged(string actionMap)
    {
        if (onActionMapChanged != null)
        {
            onActionMapChanged(actionMap);
        }
    }


    // PLAYER INPUTs
    public event Action<Vector2> onMovePressed;
    public void MovePressed(Vector2 moveDir)
    {
        if (onMovePressed != null)
        {
            onMovePressed(moveDir);
        }
    }

    public event Action onInteractPressed;
    public void InteractPressed()
    {
        if (onInteractPressed != null)
        {
            onInteractPressed();
        }
    }

    public event Action onJournalPressed;
    public void JournalPressed()
    {
        if (onJournalPressed != null)
        {
            onJournalPressed();
        }
    }


    // UI INPUTS
    public event Action onAdvancePressed;
    public void AdvancePressed()
    {
        if (onAdvancePressed != null)
        {
            onAdvancePressed();
        }
    }

    public event Action onCancelPressed;
    public void CancelPressed()
    {
        if (onCancelPressed != null)
        {
            onCancelPressed();
        }
    }


    // PUZZLE INPUTS
    public event Action<bool> onLeftClickPressed;
    public void LeftClickPressed(bool pressed)
    {
        if (onLeftClickPressed != null)
        {
            onLeftClickPressed(pressed);
        }
    }

    public event Action<bool> onRightClickPressed;
    public void RightClickPressed(bool pressed)
    {
        if (onRightClickPressed != null)
        {
            onRightClickPressed(pressed);
        }
    }
}
