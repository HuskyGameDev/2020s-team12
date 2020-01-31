using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float patrolMoveVelocity = 0.02f; // Movement speed while the enemy is patrolling
    Vector3 movement; // Movement vector
    public float point1X = 0; // The X coordinate of the desired first patrol point
    public float point1Y = 0; // The Y coordinate of the first patrol point
    public float point2X = 0; // The X coordinate of the second patrol point
    public float point2Y = 0; // The Y coordinate of the second control point
    bool movingTowardsPoint1 = true; // Whether the enemy should be heading to point 1 or 2.

    float tolerance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PatrolPoints()
    {
        if (movingTowardsPoint1)
        {
            float moveX = point1X - transform.position.x;
            float moveY = point1Y - transform.position.x;

            movement = new Vector3(moveX, moveY, 0);

            movement.Normalize();

            transform.position += (patrolMoveVelocity * movement);

            if (Mathf.Abs(moveX) <= tolerance && Mathf.Abs(moveY) <= tolerance)
            {
                print("Switcheroo!");
                movingTowardsPoint1 = false;
            }
        }
        else
        {
            float moveX = point2X - transform.position.x;
            float moveY = point2Y - transform.position.x;

            movement = new Vector3(moveX, moveY, 0);

            movement.Normalize();

            transform.position += (patrolMoveVelocity * movement);

            if (Mathf.Abs(moveX) <= tolerance && Mathf.Abs(moveY) <= tolerance)
            {
                movingTowardsPoint1 = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
