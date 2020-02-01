using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public GameObject player;

    MovingEnemy move;
    EnemyPatrol patrol;
    Health health;


    // Start is called before the first frame update
    void Start()
    {
        move = transform.parent.GetComponent<MovingEnemy>();
        patrol = transform.parent.GetComponent<EnemyPatrol>();
        health = transform.parent.GetComponent<Health>();
    }


    void OnTriggerEnter2D(Collider2D collision) // Collide with detection radius
    {
        if (collision.gameObject.Equals(player)) // If collision is with player
        {
            move.seesPlayer = true; // This enemy can now see the player
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (health.currentHealth < health.maxHealth) // Aggro if the enemy gets hit
        {
            move.seesPlayer = true; // This enemy can now see the player
        }

        if(!move.seesPlayer && patrol != null) // Patrol if enemy isn't aggro'd
        {
            patrol.PatrolPoints();
        }
    }
}
