using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TagSorting : MonoBehaviour
{
    [SerializeField] private List<SortingBucket> sortingBuckets = new();
    public UnityEvent allBucketsSorted;


    public void AddPointToBucket(int bucketIndex)
    {
        // Check to see if bucket exist
        if (bucketIndex > sortingBuckets.Count - 1)
        {
            Debug.LogWarning($"Sorting bucket {bucketIndex} not found");
            return;
        }
        SortingBucket bucket = sortingBuckets[bucketIndex];
        bucket.currentPoints++;
        bucket.pointAddedToBucket?.Invoke();
        Debug.Log($"Bucket {bucketIndex} has {bucket.currentPoints} points");
        if (bucket.currentPoints >= bucket.requiredPoints)
        {
            bucket.done = true;
            bucket.bucketDone.Invoke();
            CheckIfBucketsAreDone();
        }
    }

    public void RemovePointFromBucket(int bucketIndex)
    {
        // Check to see if bucket exist
        if (bucketIndex > sortingBuckets.Count - 1)
        {
            Debug.LogWarning($"Sorting bucket {bucketIndex} not found");
            return;
        }
        SortingBucket bucket = sortingBuckets[bucketIndex];
        bucket.currentPoints--;
        Debug.Log($"Bucket {bucketIndex} has {bucket.currentPoints} points");
        if (bucket.currentPoints < bucket.requiredPoints)
        {
            bucket.done = false;
            bucket.bucketUndone.Invoke();
        }
    }

    private void CheckIfBucketsAreDone()
    {
        foreach (SortingBucket bucket in sortingBuckets)
        {
            if (!bucket.done)
            {
                return;
            }
        }
        allBucketsSorted.Invoke();
        UiManager.Instance.ShowInGameUi();
    }
}

[Serializable]
public class SortingBucket
{
    public int requiredPoints;
    public UnityEvent bucketDone;
    public UnityEvent bucketUndone;
    public UnityEvent pointAddedToBucket;
    [HideInInspector] public int currentPoints = 0;
    [HideInInspector] public bool done = false;
}