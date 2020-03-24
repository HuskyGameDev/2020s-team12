using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBar : MonoBehaviour
{
    private Transform bar; // Creates Bar Object

    private ApplyBuff powerup;

    private float powerUpExtent;

    private float remainingDuration;

    // Start is called before the first frame update
    void Start()
    {
        if (powerup == null)
        {
            powerup = GameObject.Find("Ruby").GetComponent<ApplyBuff>(); // Sets Ruby
        }
        bar = transform.Find("Bar"); // Sets Bar Object

    }

    public void StartTickDown(float extent)
    {
        powerUpExtent = extent;
        remainingDuration = extent;
    }

    public void SetBarPercent(float power) // The Health will be the health in decimal percent ( between 0 and 1 )
    {

        bar.localScale = new Vector3(power, 1f); // Set the health bar to the proper location


    }

    private void Update()
    {
        if (remainingDuration > 0)
        {
            SetBarPercent(remainingDuration / powerUpExtent);
            remainingDuration -= Time.deltaTime;
            remainingDuration = Mathf.Clamp(remainingDuration, 0, powerUpExtent);
        }
    }

}
