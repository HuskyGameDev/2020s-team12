using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Health : MonoBehaviour
{ // This works for both the player and enemies

    public float maxHealth = 100f; // Creates and sets max health
    public float currentHealth; // Creates a variable for current health
    public float invincibilityTime = 0;
    float invincibilityTimeRemaining = 0; // To ensure Ruby can take continual damage with i-frames between

    public float knockFactor = 0; // Set in editor
    public float knockTimer = 0; // How long knockback lasts

    bool tookDamage = false;
    HealthBar bar;
    Rigidbody2D rb;
    Movement pmove;
    MovingEnemy emove;
    SpriteRenderer spriteRenderer;

    Color defaultColor;
    float flashTime = 25; // Frames of damage flash

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Sets the current health to max upon opening the game
        rb = GetComponent<Rigidbody2D>(); // Get rigid body component
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (tag == "Player")
        {
            pmove = GetComponent<Movement>(); // Get player movement component if its a player
        }
        if (tag == "Enemy")
        {
            emove = GetComponent<MovingEnemy>(); // Get enemy movement componenet
        }

        defaultColor = spriteRenderer.color;
    }

    void OnTriggerStay2D(Collider2D collision) // This is checking for collision
    {
        if (invincibilityTimeRemaining <= 0)
        {
            if (collision.GetComponent<Damage>() != null) // This allows only things with the damage script to deal damage
                // This makes sure the player can not shoot themself by comparing the tag and allowing it to go through if the tags are different
            {
                if (!collision.CompareTag(tag)) 
                {
                    Damage damage = collision.GetComponent<Damage>(); // Creates an object of the Damage Class

                    TakeDamage(damage.damageAmount); // Uses the Take Damage method and uses the damage amount from the damage class

                    StartCoroutine(KnockBack(collision, knockFactor));

                    print(tag + " Took " + damage.damageAmount); // Console print out for testing

                    tookDamage = true; // Has the enemy taken damage? Used in the detection/aggroTimer scripts to determine when the enemy should chase the player.
                }
            }
        }
    }

    bool IsDead() // This checks to see if the entity is dead, lessens code amount
    {
        return currentHealth <= 0; // Returns a boolean
    }

    IEnumerator KnockBack(Collider2D collision, float distance) // Moves player a set distance away from damage source
     {
        Vector3 damageSource = collision.transform.position; // Obtains position of damage source
        Vector3 push = transform.position - damageSource; // Obtains vector in the direction and length between player and source
        push.Normalize(); // Normalization keeps the angle, but sets the total distance to 1
        push = push * distance; // Applies given magnitude to the vector
        rb.velocity = push; // Sets the gameObject's velocity to the resulting velocity vector
        if (tag == "Player")
        {
            pmove.setMove(false); // If this is the player, keep them from moving
        }
        if (tag == "Enemy") // If this is the enemy, keep them from moving
        {
            emove.setMove(false);
        }
        yield return new WaitForSeconds(knockTimer); // wait a bit
        rb.velocity = Vector3.zero; // Stop knockback
        if (tag == "Player")
        {
            pmove.setMove(true); // Player can move again
        }
        if (tag == "Enemy")
        {
            emove.setMove(true);//enemy move again
        }


     }

    void TakeDamage(float DMG) // This Take Damage class takes in the damage amount
    {
        if (!IsDead()) // This is to keep the game from breaking when the player dies
        {
            currentHealth -= DMG; // Takes the damage amount from the health
            Mathf.Clamp(currentHealth, 0, maxHealth); // Prevents the health from dropping into negative values by placing bounds
            damageFlash(); // Flash red (or whatever) upon taking damage
            
            invincibilityTimeRemaining = invincibilityTime; //after damage taken and i-frames expire, resets time remaining.

            if (IsDead())
            {
                Die(); 
            }
        }
    }

    void Die() // Die method that deletes the object
    {
        Destroy(gameObject); // Destroys the gameobject that is being attacked
        
        if(tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("PlayerDeath"); // Plays a death sound when the player dies
        }

        print(tag + " Died"); // Prints to console for testing purposes
 
    }

    void damageFlash() // Flash color
    {
        Color flashColor = new Color(1, 0, 0); // Red
        spriteRenderer.color = flashColor; // Set sprite color to red
    }

    void updateColor() // Gradually change color to default value if the sprite isn't its normal color
    {
        if (spriteRenderer.color != defaultColor) // If the sprite isn't its normal color
        {
            float r = spriteRenderer.color.r; // current r component of color
            float g = spriteRenderer.color.g; // g compononent
            float b = spriteRenderer.color.b; // b componenent
            r -= (1f-defaultColor.r) / flashTime; // Reduce r component by a small amount
            g +=  defaultColor.g / flashTime; // Reduce g componenent by a small amount
            b += defaultColor.b / flashTime; // Reduce b "
            r = Mathf.Clamp(r, defaultColor.r, 1); // Keep r clamped to its default value
            g = Mathf.Clamp(g, 0, defaultColor.g); // g "
            b = Mathf.Clamp(b, 0, defaultColor.b); // b "

            spriteRenderer.color = new Color(r, g, b); // set the new color
        }
    }

    public float GetHealth()
    {
        return (currentHealth / 100);
    }

    public bool getTookDamage()
    {
        return tookDamage;
    }

    /* Update is called once per frame. Subtracts from i-frame time remaining while player has i-frames and changes 
    color. Once invincibility expires and number is reset, returns original color to player.
         */
    void Update()
    {

        if(invincibilityTimeRemaining > 0)
        {
            //GetComponent<SpriteRenderer>().color = Color.white;
            invincibilityTimeRemaining -= Time.deltaTime;
        }
        else if(invincibilityTime > 0)
        {
            //GetComponent<SpriteRenderer>().color = Color.green;
            //TODO Make Invincibility Animation (Flashing) and set it here
        }

        updateColor(); // update color
    }
}
