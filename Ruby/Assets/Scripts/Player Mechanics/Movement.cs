using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveVelocity = 0.125f; // Player movement speed
    bool canMove = true;

    Rigidbody2D rb;
    Animator anim;
    Vector3 movement; // Makes a 3D vector

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            float moveX = Input.GetAxis("Horizontal"); // Uses the unity tool to get button presses for the horizontal axis, allows player to change keybinding
            float moveY = Input.GetAxis("Vertical"); // Uses the unity tool to get button presses for the vertical axis, allows player to change keybinding

            movement = new Vector3(moveX, moveY, 0f); // Creates movement vector using the two axis and sets Z to 0 because the games 2D
            movement.Normalize(); // Normalizes vector so that the character doesn't go faster on diagonals

            rb.velocity = (movement * moveVelocity); // Moves the player

            anim.SetBool("Walking", (!movement.Equals(Vector3.zero))); // Tell the animator if Ruby is walking to set the appropriate sprites
        }
    }
    public void setMove(bool move)
    {
        this.canMove = move;
    }

    public bool getMove()
    {
        return canMove;
    }
}
