using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    Transform firePoint;
    public GameObject heartPrefab;
    public float fireSpeed = 0.2f;
    public float heartForce = 20f;


    // Start is called before the first frame update
    void Start()
    {

        firePoint = transform.Find("FirePoint");

    }

    void Fire()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 mouseInWorldCoords = Camera.main.ScreenToWorldPoint(mousePos);

        float angleX = mouseInWorldCoords.x - firePoint.position.x;
        float angleY = mouseInWorldCoords.y - firePoint.position.y;

        float angle = Mathf.Atan2(angleY, angleX) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        GameObject heart = Instantiate(heartPrefab, firePoint.position, rotation);

        Rigidbody2D rbheart = heart.GetComponent<Rigidbody2D>();

        rbheart.AddForce(transform.up * heartForce, ForceMode2D.Impulse);
  

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1")) {

            Fire();

        }


    }
}
