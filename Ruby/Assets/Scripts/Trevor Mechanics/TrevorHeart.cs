using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TrevorHeart : MonoBehaviour
{

    public float fireSpeed = 50f; // Fire speed of heart

    public Rigidbody2D heartRigid; // Creates a rigidbody to put the heart into the script

    public float lifeTime = 0.3f;  

    float curLifeTime = 0;

    Vector3 playerVelocity;


    // Start is called before the first frame update
    void Start()
    {
        playerVelocity = GameObject.FindObjectOfType<Movement>().gameObject.GetComponent<Rigidbody2D>().velocity;
        heartRigid.velocity = transform.up * fireSpeed + playerVelocity; // Moves the heart
        curLifeTime = lifeTime;
    }

    void OnTriggerStay2D(Collider2D collision) // Checks for collision
    {
        if (collision.GetComponent<Health>() != null && collision.CompareTag("Enemy")) // Checks to see if the object has the Health script
        {
            Destroy(gameObject); // Destroys the heart
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {
        curLifeTime -= Time.deltaTime;
        curLifeTime = Mathf.Clamp(curLifeTime, 0, lifeTime);
        if (curLifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
