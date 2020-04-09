using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    OWMS enemyAttack;
    public LayerMask obstacles;
    public bool isBoss = false;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Movement>().gameObject;
        if (isBoss)
        {
            enemyAttack = transform.parent.GetComponent<OWMS>();
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

    private bool moving = false; // Used to control use of movement script

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

            if (isBoss) // If the enemy is the boss enemy
            {
                if (!enemyAttack.isAttacking()) // As long as it isn't already attacking
                {
                    StartCoroutine(enemyAttack.ChargeAttack()); // Start charge attack
                }
            }
            else
            {
                if (!moving)
                {
                    move.MoveTowardsPlayer(); // Move towards Ruby
                    moving = true;
                    patrol.patrolVelocity = 0f;
                }
            }
        }
        else
        {
            moving = false;
            patrol.PatrolPoints(); // Otherwise, continue to patrol points
            patrol.patrolVelocity = 3f;
        }
        anim.SetBool("Walking", (!rb.velocity.Equals(Vector3.zero))); // Tell the animator if the enemy is moving to set the appropriate sprites

    }
}
