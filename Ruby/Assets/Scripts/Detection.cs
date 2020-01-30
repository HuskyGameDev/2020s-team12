using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public GameObject player;
    bool inRadius;
    MovingEnemy move;


    // Start is called before the first frame update
    void Start()
    {

        move = transform.parent.GetComponent<MovingEnemy>();

    }

    void OnTriggerEnter2D(Collider2D collision) // Collide with detection radius
    {

        if (collision.gameObject.Equals(player)) // If collision is with player
        {

            inRadius = true; // Yes

        }

    }



    // Update is called once per frame
    void Update()
    {

        if (inRadius) // Run if in radius
        {
            move.MoveTowardsPlayer(); // Move towards player
        }
    }
}
