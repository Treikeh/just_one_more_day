using UnityEngine;


public class UiTabButton : MonoBehaviour
{
    public UiTabGroup tabGroup;

    private void Start()
    {
        // Subscribe this button to the tabGroup
        tabGroup.Subscribe(this);
    }
}
