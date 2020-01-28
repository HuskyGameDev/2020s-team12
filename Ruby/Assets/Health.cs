using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float maxHealth = 100f;

    float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Damage>() != null)
        {
            if (!collision.CompareTag(tag))
            {
                Damage damage = collision.GetComponent<Damage>();

                TakeDamage(damage.damageAmount);

                print(tag + " Took " + damage.damageAmount);
            }
        }
    }


    bool IsDead()
    {
        return currentHealth <= 0;
    }

    void TakeDamage(float DMG)
    {
        if (!IsDead())
        {
            currentHealth -= DMG;
            Mathf.Clamp(currentHealth, 0, maxHealth);
            if (IsDead())
            {
                Die();
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);

        print(tag + " Died");
        //u ded
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
