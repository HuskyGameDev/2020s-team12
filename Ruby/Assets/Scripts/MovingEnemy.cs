using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    public Transform player; // Set who the player is in editor
    public float moveVelocity = .05f; // Enemy move speed
    public bool seesPlayer = false;

    Rigidbody2D rb;
    Animator anim;
    Vector3 movement; // Movement 3D vector

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void MoveTowardsPlayer()
    {
        if (player != null) // If the player doesn't exist an error will occur, so the player must exist
        {
            float moveX = player.position.x - transform.position.x; // Checks the players coordinates on the x plane in comparison to the enemies
            float moveY = player.position.y - transform.position.y; // Checks the players coordinates on the y plane in comparison to the enemies

            movement = new Vector3(moveX, moveY, 0f); // Creates a movement vector of the difference between the player and the enemy

            movement.Normalize(); // Normalizes so it's not faster on diagonals

            rb.velocity = (movement * moveVelocity); // Moves the enemy towards the player

            float facingAngle = Mathf.Atan2(moveY, moveX) * Mathf.Rad2Deg - 90f; // Sets the angle the enemy is facing in degrees
            anim.SetFloat("Facing", facingAngle); // Tell the animator which way the enemy is facing to set the appropriate sprites
        }
        else
        {
            rb.velocity = Vector3.zero; // Don't move
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
