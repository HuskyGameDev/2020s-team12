using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveVelocity = 0.125f; // Player movement speed

    Rigidbody2D rb;
    Vector3 movement; // Makes a 3D vector

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // Uses the unity tool to get button presses for the horizontal axis, allows player to change keybinding
        float moveY = Input.GetAxis("Vertical"); // Uses the unity tool to get button presses for the vertical axis, allows player to change keybinding

        movement = new Vector3(moveX, moveY, 0f); // Creates movement vector using the two axis and sets Z to 0 because the games 2D
        movement.Normalize(); // Normalizes vector so that the character doesn't go faster on diagonals

        rb.velocity = (movement * moveVelocity); // Moves the player
    }
}
