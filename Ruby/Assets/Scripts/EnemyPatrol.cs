using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float patrolMoveVelocity = .02f; // Movement speed while the enemy is patrolling
    Vector3 movement; // Movement vector
    public float point1X = 0; // The X coordinate of the desired first patrol point
    public float point1Y = 0; // The Y coordinate of the first patrol point
    public float point2X = 0; // The X coordinate of the second patrol point
    public float point2Y = 0; // The Y coordinate of the second control point
    public float atPointWaitTime = 1f; // How long the enemy waits upon reaching one of the patrol points
    bool movingTowardsPoint1 = true; // Whether the enemy should be heading to point 1 or 2

    float tolerance = .01f; // How close the enemy can be to the point for it to be called at the point

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PatrolPoints() // Makes the enemy patrol between the points
    {
        if (movingTowardsPoint1)
        {
            StartCoroutine(MoveTowardsPoint(point1X,point1Y));
        }
        else 
        {
            StartCoroutine(MoveTowardsPoint(point2X, point2Y));
        }
    }

    IEnumerator MoveTowardsPoint(float xCoord, float yCoord)
    {

        float moveX = xCoord - transform.position.x; // Calculate movement vector based on current position and the point
        float moveY = yCoord - transform.position.y;

        movement = new Vector3(moveX, moveY, 0); // Set values for movement vector

        movement.Normalize(); // Normalize vector

        if (Mathf.Abs(moveX) <= tolerance && Mathf.Abs(moveY) <= tolerance) // Once enemy reaches the desired patrol point within a given tolerance
        {
            movingTowardsPoint1 = !movingTowardsPoint1; // Start heading towards the other point
            float velocityHolder = patrolMoveVelocity;
            patrolMoveVelocity = 0; // Stop moving
            yield return new WaitForSeconds(atPointWaitTime); // Wait for a little bit based on the atPointWaitTime variable
            patrolMoveVelocity = velocityHolder; // Start moving again
        }

        transform.position += (patrolMoveVelocity * movement); // Moves the enemy
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
