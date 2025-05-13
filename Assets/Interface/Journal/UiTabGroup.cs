using System.Collections.Generic;
using UnityEngine;
// CREDITS: Game Deb Guide - Youtube: https://www.youtube.com/watch?v=211t6r12XPQ

public class UiTabGroup : MonoBehaviour
{
    public List<UiTabButton> tabButtons = new();
    public List<GameObject> objectsToSwap;


    // Add a tabButton to the tabButtons list
    public void Subscribe(UiTabButton button)
    {
        tabButtons.Add(button);
    }

    // Connect buttons to this function
    public void OnTabSelected(UiTabButton button)
    {
        // Get selected button index
        int index = button.transform.GetSiblingIndex();
        // Go through evey tab page and disable the ones that don't match "index" and enable the page that matches "index"
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
}
