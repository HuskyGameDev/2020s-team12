using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyFollow : MonoBehaviour
{
    Transform trevorTransform; // Trevor's transform
    float followSpeed; // How fast Ruby follows Trevor
    public float stopFollowRadius = 2f; // How close until Ruby stops following Trevor
    public float teleportThreshhold = 15f; // Distance at which point Ruby will teleport to Trevor's position

    // Start is called before the first frame update
    void Start()
    {
        trevorTransform = GameObject.Find("Trevor").transform;
        followSpeed = trevorTransform.gameObject.GetComponent<Movement>().moveVelocity - 1; // Set Ruby's follow speed to 1 less than Trevor's move speed
    }

    Vector2 Follow()
    {
        Vector2 moveVector = trevorTransform.position - transform.position; // Movement vector from Ruby's position to Trevor's position
        moveVector.Normalize();
        GetComponent<Rigidbody2D>().velocity = moveVector * followSpeed; // Set velocity to normalized movement vector multiplied by follow speed

        float facingAngle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg - 90f; // Angle from Ruby to Trevor

        GetComponent<Animator>().SetFloat("Facing", facingAngle); // Tell the animator which way Ruby is facing to set the appropriate sprites

        return moveVector;  
    }

    // Update is called once per frame
    void Update()
    {
        if (trevorTransform != null)
        {
            float distance = (trevorTransform.position - transform.position).magnitude;

            if (distance > stopFollowRadius)
            {
                GetComponent<Animator>().SetBool("Walking", (!Follow().Equals(Vector3.zero))); // Run the Follow method and set the walking animation for the animator
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Stop moving if within radius
                GetComponent<Animator>().SetBool("Walking", false); // No longer walking
            }

            if (distance >= teleportThreshhold)
            {
                transform.position = trevorTransform.position; // Teleport to trevor if too far away
            }

        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

    }
}
