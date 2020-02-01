using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar; // Creates Bar Object

    public Health health;

    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("Bar"); // Sets Bar Object

    }

    public void SetBarPercent(float health) // The Health will be the health in decimal percent ( between 0 and 1 )
    {

        bar.localScale = new Vector3(health, 1f); // Set the health bar to the proper location


    }

    private void Update()
    {

        SetBarPercent(health.GetHealth());
    }

}

