using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    Transform firePoint; // Makes a transform location called firePoint
    public GameObject heartPrefab; // Makes a GameObject for a heart instance
    public float fireSpeed = 0.2f; // How fast it fires


    // Start is called before the first frame update
    void Start() // Sets the firepoint at the opening of the script
    {

        firePoint = transform.Find("FirePoint"); // References the tranform object to the actual Fire Point object

    }

    void Fire() // A method for firing
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10); // Finds the mouse position on the screen
        Vector3 mouseInWorldCoords = Camera.main.ScreenToWorldPoint(mousePos); // Converts the mouse position to the position in the game world

        float angleX = mouseInWorldCoords.x - firePoint.position.x; // Compares the world position in x to the firepoint
        float angleY = mouseInWorldCoords.y - firePoint.position.y; // Compares the world position in y to the firepoint

        float angle = Mathf.Atan2(angleY, angleX) * Mathf.Rad2Deg - 90f; // Makes the angle of rotation to fire at
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // Makes a circular vector for rotation

        GameObject heart = Instantiate(heartPrefab, firePoint.position, rotation); // Instantiates a heart to fire from the firepoint to the rotation

        Rigidbody2D rbheart = heart.GetComponent<Rigidbody2D>(); // Gets the RigidBody of the heart  

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1")) { // Uses the unity editor to choose the keybind

            Fire(); // Calls the fire method

        }


    }
}
