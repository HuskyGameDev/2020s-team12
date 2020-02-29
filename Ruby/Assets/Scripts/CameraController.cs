using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CameraController : MonoBehaviour
{
    public const float roomWidth = 30f;
    public const float roomHeight = 20f;
    public Transform player; // Does not need to be set in editor

    Animator anim;
    int[] currentRoom = { 0, 0 }; // Room coords X and Y

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
            player = GameObject.Find("Ruby").transform; // Sets Ruby
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relativePos = player.position - transform.position;

        if(relativePos.x > (roomWidth / 2f)) // Player moving to right room
        {
            transform.position += (Vector3.right * roomWidth);
            anim.SetTrigger("MoveRight");
        }
        else if(relativePos.x < -(roomWidth / 2f))
        {
            transform.position += (Vector3.left * roomWidth);
            anim.SetTrigger("MoveLeft");
        }
        else if (relativePos.y > (roomHeight / 2f - 2.5f))
        {
            transform.position += (Vector3.up * roomHeight);
            anim.SetTrigger("MoveUp");
        }
        else if (relativePos.y < -(roomHeight / 2f + 2.5f))
        {
            transform.position += (Vector3.down * roomHeight);
            anim.SetTrigger("MoveDown");
        }
    }
}
