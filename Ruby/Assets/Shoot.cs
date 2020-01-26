using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Rigidbody2D heart;
    public float fireSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Fire()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 mouseInWorldCoords = Camera.main.ScreenToWorldPoint(mousePos);
        print(mouseInWorldCoords.x + " " + mouseInWorldCoords.y);

        float angleX = mouseInWorldCoords.x - transform.position.x;
        float angleY = mouseInWorldCoords.y - transform.position.y;

        //skrt
        //print(angleX + " " + angleY);

        float angle = Mathf.Atan2(angleY, angleX) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Rigidbody2D heartInstance = (Rigidbody2D)Instantiate(heart, transform.position, rotation);

        heartInstance.velocity = heartInstance.transform.up * fireSpeed;

  

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1")) {

            Fire();

        }


    }
}
