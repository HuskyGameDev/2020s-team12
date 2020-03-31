using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrevorShoot : MonoBehaviour
{
    Transform firePoint; // Makes a transform location called firePoint
    public GameObject heartPrefab; // Makes a GameObject for a heart instance
    public float fireSpeed = 0.2f; // How fast the bullet moves
    public float fireRate; // How fast you can fire (in bullets per second)
    public Sound shootSound;
    float currentFireCooldown = 0; // How much time until the player can shoot again
    float aimingAngle = 180f; // The angle in degrees the player is aiming/facing (0 as north, -90 as east, -180 as south, 90/-270 as west)
    int spreadMultiplier = 9;
    Animator anim;

    // Start is called before the first frame update
    void Start() // Sets the firepoint at the opening of the script
    {
        firePoint = transform.Find("FirePoint"); // References the tranform object to the actual Fire Point object
        anim = GetComponent<Animator>();
    }

    public void setFireRate(float firerate)
    {
        fireRate = firerate;
    }
    public void setSpreadMultiplier(int spread)
    {
        spreadMultiplier = spread;
    }


    void Aim()
    {
        if (!PauseMenu.gamePaused)
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10); // Finds the mouse position on the screen
            Vector3 mouseInWorldCoords = Camera.main.ScreenToWorldPoint(mousePos); // Converts the mouse position to the position in the game world

            float angleX = mouseInWorldCoords.x - firePoint.position.x; // Compares the world position in x to the firepoint
            float angleY = mouseInWorldCoords.y - firePoint.position.y; // Compares the world position in y to the firepoint

            aimingAngle = Mathf.Atan2(angleY, angleX) * Mathf.Rad2Deg - 90f; // Makes the angle of rotation to fire at

            anim.SetFloat("Facing", aimingAngle); // Tell the animator which way Ruby is facing to set the appropriate sprites
        }
    }

    void Fire(Quaternion bulletRotation) // A method for firing
    {
        if (!PauseMenu.gamePaused)
        {
            if (shootSound != null)
            {
                AudioSource.PlayClipAtPoint(shootSound.clip, firePoint.position);
            }
            GameObject heart = Instantiate(heartPrefab, firePoint.position, bulletRotation); // Instantiates a heart to fire from the firepoint to the rotation

            Rigidbody2D rbheart = heart.GetComponent<Rigidbody2D>(); // Gets the RigidBody of the heart

            currentFireCooldown = 60 / fireRate; // Set the cooldown to be the inverse of the fireRate in seconds (fireRate is the desired amount of bullets to be able to shot in a 60 frame [1 second] timeframe)


        }
    }

    // Update is called once per frame
    void Update()
    {
        Aim();

        if (Input.GetButton("Shoot") && (currentFireCooldown == 0)) // Uses the unity editor to choose the keybind. Can't fire unless currentFireCooldown is 0
        {
            for (int i = 0; i < spreadMultiplier; i++)
            {
                Fire(Quaternion.Euler(new Vector3(0, 0, aimingAngle + ((i+1)/2 * 5 * Mathf.Pow(-1,i))))); // Calls the fire method with the rotation needed for the bullet
            }

        }

        currentFireCooldown -= Time.deltaTime; // Reduce the current cooldown
        currentFireCooldown = Mathf.Clamp(currentFireCooldown, 0, (1 / fireRate)); // Prevents currentFireCooldown from going below 0 and above (60/ fireRate)


    }
}
