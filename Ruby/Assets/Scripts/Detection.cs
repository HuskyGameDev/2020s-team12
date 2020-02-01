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


    // Start is called before the first frame update
    void Start()
    {
        move = transform.parent.GetComponent<MovingEnemy>();
        patrol = transform.parent.GetComponent<EnemyPatrol>();
        health = transform.parent.GetComponent<Health>();
        aggroTimer = transform.parent.GetComponent<AggroTimer>();
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
        if (inRadius || health.tookDamage) // Run if in radius
        {
            aggroTimer.StartAggro();
            inRadius = false;
            health.tookDamage = false;
        }

        if (aggroTimer.isAggro)
        {
            move.MoveTowardsPlayer(); // Move towards Ruby
        }
        else
        {
            patrol.PatrolPoints();
        }
    }
}
