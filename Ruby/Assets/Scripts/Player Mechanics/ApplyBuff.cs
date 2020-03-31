using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBuff : MonoBehaviour
{
    public float rapidFireRate; // Rate at which rapid fire shoots

    public int spreadShotMultiplier = 3; // Number of Shots during spread shot power up
    private PowerBar powerBar;
    private GameObject bar;
    private GameObject backBar;
    public ApplyBuff.BuffType currentPower;
    private ImageControl icontrol;
    private GameObject icont;
    float defaultFireRate;
    Shoot shoot;
    
    public enum BuffType
    {
        RapidFire,
        HealthPickup,
        MultiAttack,
        Default
    }


    // Start is called before the first frame update
    void Start()
    {
        currentPower = BuffType.Default;
        if (powerBar == null)
        {
            backBar = GameObject.Find("/UI/BackBarPower");
            bar = GameObject.Find("/UI/BackBarPower/PowerBar");
            powerBar = bar.GetComponent<PowerBar>();
        }
        if (icontrol == null)
        {
            icont = GameObject.Find("/UI/BackBarPower/TheImage");
            icontrol = icont.GetComponent<ImageControl>();
        }
        shoot = GetComponent<Shoot>();
        defaultFireRate = shoot.fireRate;
        backBar.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D collision) // When object with this script collides with 'powerup' object
    {
        

        PowerUp pow = collision.GetComponent<PowerUp>();

        if (pow != null)//Checks if the collision object (The powerup) has PowerUp component
        {
            backBar.SetActive(true);
            StartCoroutine(ApplyEffect(pow));
            Destroy(collision.gameObject); // Destroys powerup after use
        }

    }

    public void CheckIfActive(BuffType type)
    {
        if (currentPower.Equals(type))
        {
            currentPower = BuffType.Default;
            backBar.SetActive(false);
        }
    }
 

    IEnumerator ApplyEffect(PowerUp power) {
        


        currentPower = power.type;
       
        icontrol.ChangeImage();

        switch (power.type) {

            case BuffType.RapidFire:
                shoot.setFireRate(rapidFireRate); // Set fire rate located in shoot script
                powerBar.StartTickDown(power.powerupExtent);
                yield return new WaitForSeconds(power.powerupExtent); // Waits using powerupExtend (seconds)
                shoot.setFireRate(defaultFireRate); // Returns fire rate to default
                
                icontrol.ChangeImage();
                CheckIfActive(power.type);
                break;


            case BuffType.HealthPickup:
                Health health = GetComponent<Health>();
                if (health.currentHealth <= health.currentHealth - power.healAmount)
                {
                    health.currentHealth = health.currentHealth + power.healAmount;//Adds value to current health
                }
                else
                {
                    health.currentHealth = health.maxHealth;
                }
                break;

            case BuffType.MultiAttack:
                Shoot shooot = GetComponent<Shoot>();
                shooot.setSpreadMultiplier(spreadShotMultiplier);
                powerBar.StartTickDown(power.powerupExtent);
                yield return new WaitForSeconds(power.powerupExtent);
                shooot.setSpreadMultiplier(1);
                
                icontrol.ChangeImage();
                CheckIfActive(power.type);
                break;
                //Can add more types of powerups here

        }
        
    }
    
}
