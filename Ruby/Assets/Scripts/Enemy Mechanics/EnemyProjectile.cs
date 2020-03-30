using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyProjectile : MonoBehaviour
{
    public float fireSpeed = 20f; // Fire speed of heart

    public Rigidbody2D projectileRigid; // Creates a rigidbody to put the heart into the script


    // Start is called before the first frame update
    void Start()
    {

        projectileRigid.velocity = transform.up * fireSpeed; // Moves the heart

    }

    void OnTriggerStay2D(Collider2D collision) // Checks for collision
    {
        if (collision.GetComponent<Health>() != null && collision.CompareTag("Player")) // Checks to see if the object has the Health script
        {
            Destroy(gameObject); // Destroys the heart
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
