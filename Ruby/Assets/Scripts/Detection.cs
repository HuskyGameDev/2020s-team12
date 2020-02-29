using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Detection : MonoBehaviour
{
    public GameObject player;

    MovingEnemy move;
    EnemyPatrol patrol;
    Health health;
    AggroTimer aggroTimer;
    Animator anim;
    Rigidbody2D rb;


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
    }


    void OnTriggerStay2D(Collider2D collision) // Collide with detection radius
    {
        if (collision.gameObject.Equals(player)) // If collision is with player
        {
            move.seesPlayer = true; // This enemy can now see the player
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (move.seesPlayer || health.tookDamage) // Run if in radius or if taking damage from the player
        {
            aggroTimer.StartAggro(); // Start chasing the player
            move.seesPlayer = false; // reset these boolean values
            health.tookDamage = false;
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
