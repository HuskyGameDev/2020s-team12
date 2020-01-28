using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour { 

    public float fireSpeed = 10f;

    public Rigidbody2D heartRigid;


    // Start is called before the first frame update
    void Start()
    {

        heartRigid.velocity = transform.up * fireSpeed;
   
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Destroy(gameObject);
    }


}
