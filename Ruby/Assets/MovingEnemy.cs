using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    public Transform player; // Set in editor

    public float moveVelocity = .05f;

    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void MoveTowardsPlayer()
    {
        float moveX = player.position.x - transform.position.x;
        float moveY = player.position.y - transform.position.y;

        movement = new Vector3(moveX, moveY, 0f);

        movement.Normalize();

        transform.position += (moveVelocity * movement);
    }
    // Update is called once per frame
    void Update()
    {

        if(player != null)
        {
            MoveTowardsPlayer();
        }
        
    }
}
