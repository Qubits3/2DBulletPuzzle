using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField]private GameObject[] waypoints;
    private int currentWaypointIndex;

    [SerializeField]private float speed = 1f;
    void Update()
    {
        MoveBetweenWaypoints();
    }
    private void MoveBetweenWaypoints()
    {
        if(Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .02f)
        {
            currentWaypointIndex++;
            if(currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, 
            Time.deltaTime * speed);
    }
}
