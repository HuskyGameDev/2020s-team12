using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBuff : MonoBehaviour
{
    public int powerupExtent;//how long in seconds powerup lasts
    public float rapidFireRate;//rate at which rapid fire shoots

    public enum BuffType
    {
        RapidFire
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)//When object with this script collides with 'powerup' object
    {
        print("enter pp up");

        if (collision.tag == "Powerup")
        {
            StartCoroutine(ApplyEffect(BuffType.RapidFire));
        }

        Destroy(collision.gameObject);//destroys powerup after use

    }


    IEnumerator ApplyEffect(BuffType type)
    {
        Shoot shoot = GetComponent<Shoot>();
        shoot.setFireRate(rapidFireRate);//Set fire rate located in shoot script


        yield return new WaitForSeconds(powerupExtent);//waits using powerupExtend (seconds)
        shoot.setFireRate(shoot.defaultFireRate);//returns fire rate to default
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
