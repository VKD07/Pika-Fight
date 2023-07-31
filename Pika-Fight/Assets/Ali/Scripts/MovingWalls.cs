using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWalls : MonoBehaviour
{
    [SerializeField]
    Transform[] waypoints;

    [SerializeField]
    float moveSpeed = 5f;

    int waypointIndex = 0;

    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
    }


    void Update()
    {
        pointsmove();
    }

    void pointsmove()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

        if (transform.position == waypoints[waypointIndex].transform.position)
        {
            waypointIndex += 1;
        }

        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
}
