using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Detection : MonoBehaviour
{
    GameObject player;
    MovingEnemy move;
    EnemyPatrol patrol;
    Health health;
    AggroTimer aggroTimer;
    Animator anim;
    Rigidbody2D rb;
    public LayerMask obstacles;


    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Ruby"); // Sets Ruby
        }
        move = transform.parent.GetComponent<MovingEnemy>();
        patrol = transform.parent.GetComponent<EnemyPatrol>();
        health = transform.parent.GetComponent<Health>();
        aggroTimer = transform.parent.GetComponent<AggroTimer>();
        anim = transform.parent.GetComponent<Animator>();
        rb = transform.parent.GetComponent<Rigidbody2D>();
        obstacles = 1 << 8; // This sets the layerMask to the "Obstacle" unity layer. It's a literal bit mask. Ask Kasey if you need clarification.
    }

    /*
     * Sends a raycast between this enemy and the player. If it does not collide with
     * any obstacles in the obstacle layermask, then it will return true. If it collides,
     * it returns false.
     */
    bool IsVisible()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized; // get direction
        float distance = Vector3.Distance(transform.position, player.transform.position);
        // If raycast hits target
        if (Physics2D.Raycast(transform.position, direction, distance, obstacles))
        {
            return false;
        } 
        else
        {
            return true;
        }
    }

    void OnTriggerStay2D(Collider2D collision) // Collide with detection radius
    {
        if (collision.gameObject.Equals(player)) // If collision is with player
        {
            if (IsVisible())
            {
                move.setSee(true); // This enemy can now see the player
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (move.getSee().Equals(true) || health.getTookDamage().Equals(true)) // Run if in radius or if taking damage from the player
        {
            aggroTimer.StartAggro(); // Start chasing the player
            move.setSee(false); // reset these boolean values
            health.setTookDamage(false);
        }

        if (aggroTimer.isAggro) // If the enemy is currently targeting Ruby
        {
            move.MoveTowardsPlayer(); // Move towards Ruby
        }
        else
        {
            patrol.PatrolPoints(); // Otherwise, continue to patrol points
        }
        anim.SetBool("Walking", (!rb.velocity.Equals(Vector3.zero))); // Tell the animator if the enemy is moving to set the appropriate sprites

    }
}
