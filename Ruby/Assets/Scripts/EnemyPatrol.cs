using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float patrolMoveVelocity = 1f; // Movement speed while the enemy is patrolling
    Vector3 movement; // Movement vector
    public float relativePoint1X = 0; // The relative X coordinate of the first patrol point
    public float relativePoint1Y = 0; // The relative Y coordinate of the first patrol point
    public float relativePoint2X = 0; // The relative X coordinate of the second patrol point
    public float relativePoint2Y = 0; // The relative Y coordinate of the second patrol point

    public bool onePointPatrol = false;

    float point1X = 0; // The actual coordinates of the first patrol point
    float point1Y = 0;
    float point2X = 0; // The actual coordinates of the second patrol point
    float point2Y = 0;
    public float atPointWaitTime = 1f; // How long the enemy waits upon reaching one of the patrol points
    bool movingTowardsPoint1 = true; // Whether the enemy should be heading to point 1 or 2
    Rigidbody2D rb;
    Animator anim;

    float tolerance = .1f; // How close the enemy can be to the point for it to be called at the point

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>(); // get the components
        anim = transform.GetComponent<Animator>();

        float startX = transform.position.x; // Get the initial coordinates of the enemy to convert the relative coordinates to actual coordinates
        float startY = transform.position.y;
        point1X = startX + relativePoint1X;
        point1Y = startY + relativePoint1Y;
        point2X = startX + relativePoint2X;
        point2Y = startY + relativePoint2Y;
    }

    public void PatrolPoints() // Makes the enemy patrol between the points
    {
        if (onePointPatrol)
        {
            StartCoroutine(MoveTowardsPoint(point1X, point1Y));
        }
        else
        {
            if (movingTowardsPoint1)
            {
                StartCoroutine(MoveTowardsPoint(point1X, point1Y));
            }
            else
            {
                StartCoroutine(MoveTowardsPoint(point2X, point2Y));
            }
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

            if (!onePointPatrol)
            {
                yield return new WaitForSeconds(atPointWaitTime); // Wait for a little bit based on the atPointWaitTime variable
                patrolMoveVelocity = velocityHolder; // Start moving again
            }
        }
        

        rb.velocity = movement * patrolMoveVelocity; // Moves the enemy
        float facingAngle = Mathf.Atan2(moveY, moveX) * Mathf.Rad2Deg - 90f; // Sets the angle the enemy is facing in degrees
        anim.SetFloat("Facing", facingAngle); // Tell the animator which way the enemy is facing to set the appropriate sprites
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
