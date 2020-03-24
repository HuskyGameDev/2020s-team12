using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OWMS : MonoBehaviour
{
    public float moveAwayVelocity = 2;
    public float chargeVelocity = 10;
    public float moveAwayTime = 2; // Pause time in seconds before charging
    public float crashStun = 1;
    float currentMoveAwayTime = 0;
    bool attacking = false;

    Rigidbody2D rb;
    Transform playerPos;
    SpriteRenderer enemySprite;
    Color initColor;


    // Start is called before the first frame update
    void Start()
    {
        if (playerPos == null)
        {
            playerPos = GameObject.Find("Ruby").GetComponent<Transform>();
        }
        rb = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
        initColor = enemySprite.color;
    }

    public bool isAttacking()
    {
        return attacking;
    }
    void TeleportBehind()
    {

    }

    public IEnumerator ChargeAttack()
    {
        attacking = true;

        bool attackRandomizer = (Random.value > .5f);
        print(attackRandomizer);
        if (attackRandomizer)
        {
            float elapsedTime = 0;
            Vector2 tpLocation = playerPos.position;
            while (elapsedTime < moveAwayTime)
            {
                MoveAway();
                yield return new WaitForEndOfFrame();
                float newTransparency = Mathf.Clamp(initColor.a * Time.deltaTime / moveAwayTime, 0, initColor.a);
                enemySprite.color -= new Color(0, 0, 0, newTransparency);
                elapsedTime += Time.deltaTime;
            }
            enemySprite.color = initColor;
            transform.position = tpLocation;
        }
        else
        {
            MoveAway();
            yield return new WaitForSeconds(moveAwayTime);
        }

        do
        {
            Vector2 chargeVector = playerPos.position - transform.position;
            chargeVector.Normalize();
            rb.velocity = chargeVector * chargeVelocity;
            yield return new WaitUntil(() => rb.velocity.x.Equals(0) | rb.velocity.y.Equals(0));
        } while (Random.value < .6f);

        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(crashStun);

        float elapsedTime2 = 0;
        while (elapsedTime2 < moveAwayTime)
        {
            MoveTowards();
            yield return new WaitForEndOfFrame();
            elapsedTime2 += Time.deltaTime;
        }

        attacking = false;
    }

    void MoveAway()
    {
        Vector2 move = -(playerPos.position - transform.position);
        move.Normalize();
        rb.velocity = move * moveAwayVelocity;
    }

    void MoveTowards()
    {
        Vector2 move = playerPos.position - transform.position;
        move.Normalize();
        rb.velocity = move * moveAwayVelocity;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
