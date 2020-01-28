using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{ // This works for both the player and enemies

    public float maxHealth = 100f; // Creates and sets max health
    float currentHealth; // Creates a variable for current health

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Sets the current health to max upon opening the game
    }

    void OnTriggerEnter2D(Collider2D collision) // This is checking for collision
    {
        if (collision.GetComponent<Damage>() != null) // This takes the collision detector from the damage script, which is on the bullet and the enemies
        { 
           if (!collision.CompareTag(tag)) // This makes sure the player can not shoot themself by comparing the tag and allowing it to go through if the tags are different
            {
                Damage damage = collision.GetComponent<Damage>(); // Creates an object of the damage class

                TakeDamage(damage.damageAmount); // Uses the Take Damage method and uses the damage amount from the damage class

                print(tag + " Took " + damage.damageAmount); // Console print out for testing
            }
        }
    }


    bool IsDead() // This checks to see if the entity is dead, lessens code amount
    {
        return currentHealth <= 0; // Returns a boolean
    }

    void TakeDamage(float DMG) // This Take Damage class takes in the damage amount
    {
        if (!IsDead()) // This is to keep the game from breaking when the player dies
        {
            currentHealth -= DMG; // Takes the damage amount from the health
            Mathf.Clamp(currentHealth, 0, maxHealth); // Prevents the health from dropping into negative values by placing bounds
            if (IsDead()) // If dead, then die
            {
                Die(); // Death
            }
        }
    }

    void Die() // Die method that deletes the object
    {
        Destroy(gameObject); // Destroys the gameobject that is being attacked

        print(tag + " Died"); // Prints to console for testing purposes
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
