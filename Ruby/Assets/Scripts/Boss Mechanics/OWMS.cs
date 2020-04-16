using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OWMS : MonoBehaviour // I apologize for the naming scheme, it was the only thing I could think of while writing the teleport behind mechanic. If you get what it stands for then you're a weeb.
{
    public float moveAwayVelocity = 2; // How fast the enemy moves away from Ruby
    public float chargeVelocity = 10; // How fast the enemy charges towrads Ruby
    public float moveAwayTime = 2; // Pause time in seconds before charging
    public float crashStun = 1; // How long the enemy is stunned upon finally crashing into a wall
    bool attacking = false; // Whether or not the enemy is in the middle of executing an attack (Used in detection for behavior decision making)
    float minVelocity = 4; // The minimum velocity the boss should be charging at

    Rigidbody2D rb;
    Transform playerPos;
    SpriteRenderer enemySprite;
    Color initColor;


    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindObjectOfType<Movement>().gameObject.transform;
        rb = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
        initColor = enemySprite.color;
    }

    public bool isAttacking()
    {
        return attacking;
    }

    public IEnumerator ChargeAttack()
    {
        attacking = true; // Is now attacking


        bool attackRandomizer = (Random.value > .5f); // Randomizer variable (50/50) Determines whether or not the enemy teleports before charging

        if (attackRandomizer)
        {
            FindObjectOfType<AudioManager>().Play("SnakeHiss1");
        }

        if (attackRandomizer) // Teleport attack
        {
            float elapsedTime = 0;
            Vector2 tpLocation = playerPos.position; // Get the player's position at this point in time
            while (elapsedTime < moveAwayTime) // Do this until moveAwayTime has elapsed
            {
                MoveAway();
                yield return new WaitForEndOfFrame(); // Wait a frame
                float newTransparency = Mathf.Clamp(initColor.a * Time.deltaTime / moveAwayTime, 0, initColor.a); // create a Color var with a decreased transparency
                enemySprite.color -= new Color(0, 0, 0, newTransparency); // Set the boss's color to the new less transparent color
                elapsedTime += Time.deltaTime; // Increment elapsed time
            }
            enemySprite.color = initColor; // Return color to normal
            transform.position = tpLocation; // Teleport to the position Ruby was in a [moveAwayTime] seconds ago
        }
        else // Don't teleport
        {
            MoveAway();
            yield return new WaitForSeconds(moveAwayTime);
        }

        do // Charge at Ruby
        {
            Vector2 chargeVector = playerPos.position - transform.position;
            chargeVector.Normalize();
            rb.velocity = chargeVector * chargeVelocity;
            yield return new WaitUntil(() => rb.velocity.x.Equals(0) | rb.velocity.y.Equals(0) | rb.velocity.magnitude < minVelocity); // Wait until the boss hits a wall
        } while (Random.value < .6f); // 60% chance to bounce and charge towards Ruby again

        rb.velocity = Vector2.zero; // Stop moving
        yield return new WaitForSeconds(crashStun); // Be stunned for a bit

        float elapsedTime2 = 0;
        while (elapsedTime2 < moveAwayTime) // Move towards Ruby a little bit
        {
            MoveTowards();
            yield return new WaitForEndOfFrame();
            elapsedTime2 += Time.deltaTime;
        }

        attacking = false; // Done attacking
    }

    void MoveAway()
    {
        Vector2 move = -(playerPos.position - transform.position);
        move.Normalize();
        rb.velocity = move * moveAwayVelocity;
    }

    void MoveTowards()
    {
        if (playerPos != null)
        {
            Vector2 move = playerPos.position - transform.position;
            move.Normalize();
            rb.velocity = move * moveAwayVelocity;
        }
    }

    private void OnDestroy()
    {
        if (gameObject.GetComponentInChildren<Detection>().isBoss)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
