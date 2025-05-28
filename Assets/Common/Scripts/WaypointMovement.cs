using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints = new();
    [SerializeField] private bool moveOnStart = false;
    [SerializeField] private float moveSpeed = 10f;

    private bool isMoving = false;
    private int waypointIndex = 0;
    private Vector2 targetPosition;


    private void Start()
    {
        if (moveOnStart)
        {
            StartMoving();
        }
    }


    private void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
            NextWaypointIndex();
            }
        }
    }

    private void NextWaypointIndex()
    {
        if (waypointIndex < waypoints.Count - 1)
        {
            waypointIndex++;
            targetPosition = waypoints[waypointIndex].position;
        }
    }


    public void StartMoving()
    {
        isMoving = true;
        targetPosition = waypoints[waypointIndex].position;
    }
}
