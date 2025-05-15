using System;
using System.Collections.Generic;
using UnityEngine;


// This component uses the story progress integer in QuestManager to activate objects when the story is within a range

public class StoryProgressSetter : MonoBehaviour
{
    [SerializeField] private List<StoryProgressState> storyStates = new();


    private void Start()
    {
        SetStoryState();
    }

    public void SetStoryState()
    {
        int storyProgress = QuestManager.Instance.storyProgress;
        foreach (StoryProgressState state in storyStates)
        {
            // Check if the story is within the range defined in the state
            bool withinRange = storyProgress >= state.minStoryProgress && storyProgress <= state.maxStoryProgress;
            state.objectToActivate.SetActive(withinRange);
        }
    }

    [Serializable]
    public class StoryProgressState
    {
        public GameObject objectToActivate;
        public int minStoryProgress = 0;
        public int maxStoryProgress = 0;
    }
}
