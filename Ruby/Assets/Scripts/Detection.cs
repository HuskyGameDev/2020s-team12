using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public GameObject player;

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
            move.seesPlayer = true; // This enemy can now see the player
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
