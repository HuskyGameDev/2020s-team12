using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour { 

    public float fireSpeed = 10f; // Fire speed of heart

    public Rigidbody2D heartRigid; // Creates a rigidbody to put the heart into the script


    // Start is called before the first frame update
    void Start()
    {

        heartRigid.velocity = transform.up * fireSpeed; // Moves the heart
   
    }

    void OnTriggerEnter2D(Collider2D hitInfo) // Checks for collision
    {
        Destroy(gameObject); // Destroys the heart
    }


}
