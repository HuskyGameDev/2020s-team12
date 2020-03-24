using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBuff : MonoBehaviour
{
    public float rapidFireRate; // Rate at which rapid fire shoots

    public int spreadShotMultiplier = 3; // Number of Shots during spread shot power up
    public PowerBar powerBar;
    public ApplyBuff.BuffType currentPower;
    public ImageControl icontrol;
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
        shoot = GetComponent<Shoot>();
        defaultFireRate = shoot.fireRate;
    }

    void OnTriggerStay2D(Collider2D collision) // When object with this script collides with 'powerup' object
    {
       

        PowerUp pow = collision.GetComponent<PowerUp>();

        if (pow != null)//Checks if the collision object (The powerup) has PowerUp component
        {
            print("got my pp up");
            StartCoroutine(ApplyEffect(pow));
            Destroy(collision.gameObject); // Destroys powerup after use
        }

    }

 



    IEnumerator ApplyEffect(PowerUp power) {

        currentPower = power.type;
       
        icontrol.changeImage();

        switch (power.type) {

            case BuffType.RapidFire:

                shoot.setFireRate(rapidFireRate); // Set fire rate located in shoot script
                powerBar.StartTickDown(power.powerupExtent);
                yield return new WaitForSeconds(power.powerupExtent); // Waits using powerupExtend (seconds)
                shoot.setFireRate(shoot.defaultFireRate); // Returns fire rate to default
                currentPower = BuffType.Default;
                icontrol.changeImage();
                break;


            case BuffType.HealthPickup:
                Health health = GetComponent<Health>();
                health.currentHealth = health.currentHealth + power.healAmount;//Adds value to current health
                break;

            case BuffType.MultiAttack:
                Shoot shooot = GetComponent<Shoot>();
                print("Now that's a lot of damage");
                shooot.setSpreadMultiplier(spreadShotMultiplier);
                powerBar.StartTickDown(power.powerupExtent);
                yield return new WaitForSeconds(power.powerupExtent);
                shooot.setSpreadMultiplier(1);
                currentPower = BuffType.Default;
                icontrol.changeImage();
                break;
                //Can add more types of powerups here

        }
        
    }
    
}
