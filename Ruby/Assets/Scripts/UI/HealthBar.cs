using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    private Transform bar; // Creates Bar Object

    private Health health;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneOrganizer.trevorScenes.Contains(SceneManager.GetActiveScene()) && health == null)
        {
            health = GameObject.Find("Trevor").GetComponent<Health>(); // Sets Trevors' Health if in MapA Scene
        }
        else if (health == null)
        {
            health = GameObject.Find("Ruby").GetComponent<Health>(); // Sets Ruby
        }
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

