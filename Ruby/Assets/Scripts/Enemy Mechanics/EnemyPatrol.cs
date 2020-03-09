using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float patrolVelocity = 1f; // Movement speed while the enemy is patrolling
    float currVelocity; // current velocity while patrolling
    Vector3 movement; // Movement vector
    public List<Vector2> relativePatrolPoints;
    List<Vector2> absolutePatrolPoints;

    public float atPointWaitTime = 1f; // How long the enemy waits upon reaching one of the patrol points
    bool movingTowardsPoint1 = true; // Whether the enemy should be heading to point 1 or 2
    bool moving = true; // If the enemy should be moving
    Rigidbody2D rb;
    Animator anim;

    float tolerance = .1f; // How close the enemy can be to the point for it to be called at the point
    int pointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>(); // get the components
        anim = transform.GetComponent<Animator>();

        currVelocity = patrolVelocity;

        float startX = transform.position.x; // Get the initial coordinates of the enemy to convert the relative coordinates to actual coordinates
        float startY = transform.position.y;

        absolutePatrolPoints = new List<Vector2>();
        for (int i = 0; i < relativePatrolPoints.Count; i++)
        {
            absolutePatrolPoints.Add(relativePatrolPoints[i] + (new Vector2(startX, startY)));
        }

    }

    public void PatrolPoints() // Makes the enemy patrol between the points
    {
        if (absolutePatrolPoints.Count == 1)
        {
            StartCoroutine(MoveTowardsPoint(absolutePatrolPoints[0]));
        }
        else
        {
            StartCoroutine(MoveTowardsPoint(absolutePatrolPoints[pointIndex]));
        }
    }

    IEnumerator MoveTowardsPoint(Vector2 point)
    {
        float moveX = point.x - transform.position.x; // Calculate movement vector based on current position and the point
        float moveY = point.y - transform.position.y;


        movement = new Vector3(moveX, moveY, 0); // Set values for movement vector

        movement.Normalize(); // Normalize vector

        if (Mathf.Abs(moveX) <= tolerance && Mathf.Abs(moveY) <= tolerance) // Once enemy reaches the desired patrol point within a given tolerance
        {
            movingTowardsPoint1 = !movingTowardsPoint1; // Start heading towards the other point
            currVelocity = 0; // Stop moving

            if (absolutePatrolPoints.Count != 0)
            {
                moving = false; // Don't move
                yield return new WaitForSeconds(atPointWaitTime); // Wait for a little bit based on the atPointWaitTime variable
                moving = true; // Move
                currVelocity = patrolVelocity; // Start moving again
            }

            pointIndex = ((pointIndex + 1) >= absolutePatrolPoints.Count) ? 0 : pointIndex + 1; // If enemy has patrolled all the points, go back to the first point
        }
        else if(moving)
        {
            currVelocity = patrolVelocity;
        }
        

        rb.velocity = movement * currVelocity; // Moves the enemy
        float facingAngle = Mathf.Atan2(moveY, moveX) * Mathf.Rad2Deg - 90f; // Sets the angle the enemy is facing in degrees
        anim.SetFloat("Facing", facingAngle); // Tell the animator which way the enemy is facing to set the appropriate sprites
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
