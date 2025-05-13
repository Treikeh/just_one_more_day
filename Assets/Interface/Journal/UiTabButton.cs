using UnityEngine;


public class UiTabButton : MonoBehaviour
{
    public UiTabGroup tabGroup;

    private void Start()
    {
        tabGroup.Subscribe(this);
    }
}
