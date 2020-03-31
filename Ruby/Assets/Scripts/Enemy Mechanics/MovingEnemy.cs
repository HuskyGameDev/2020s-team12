using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingEnemy : MonoBehaviour
{
    Transform player; // Does not need to be set in editor
    public float moveVelocity = .05f; // Enemy move speed
    public bool seesPlayer = false;
    public bool canMove = true;

    Rigidbody2D rb;
    Animator anim;
    Vector3 movement; // Movement 3D vector

    // Start is called before the first frame update
    void Start()
    {
        if (player == null && SceneManager.GetActiveScene().name.Equals("MapA"))
        {
            player = GameObject.Find("Trevor").transform; // Sets trevor if MapA Scene
        }
        else if (player == null) { 
            player = GameObject.Find("Ruby").transform; // Sets Ruby
        }
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void MoveTowardsPlayer()
    {
        if (player != null && canMove) // If the player doesn't exist an error will occur, so the player must exist
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
                rb.velocity = Vector3.zero;
           
            }
        }
       
    public void setMove(bool move)
    {
        this.canMove = move;
    }

    public bool getMove()
    {
        return canMove;
    }

    public void setSee(bool see)
    {
        this.seesPlayer = see;
    }

    public bool getSee()
    {
        return seesPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = rb.velocity.x;
        float moveY = rb.velocity.y;
        float facingAngle = Mathf.Atan2(moveY, moveX) * Mathf.Rad2Deg - 90f; // Sets the angle the enemy is facing in degrees
        anim.SetFloat("Facing", facingAngle); // Tell the animator which way the enemy is facing to set the appropriate sprites
    }
}
