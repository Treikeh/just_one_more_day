using System.Collections.Generic;
using UnityEngine;
// CREDITS: Game Deb Guide - Youtube: https://www.youtube.com/watch?v=211t6r12XPQ

public class UiTabGroup : MonoBehaviour
{
    public List<UiTabButton> tabButtons;
    public List<GameObject> objectsToSwap;

    public void Subscribe(UiTabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<UiTabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabSelected(UiTabButton button)
    {
        ResetTabs();
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (UiTabButton button in tabButtons)
        {
            //
        }
    }
}
