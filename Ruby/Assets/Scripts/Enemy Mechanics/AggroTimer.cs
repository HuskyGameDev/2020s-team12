using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroTimer : MonoBehaviour
{
    public float aggroTime = 1; // How long the enemy is aggro'd in seconds upon being hit/detecting Ruby
    public bool isAggro = false; // Is the enemy currently chasing Ruby?
    float currentAggroTime = 0; // How long until it stops chasing Ruby

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartAggro() // Start chasing the player
    {
        currentAggroTime = aggroTime; // Set the aggro time
    }

    // Update is called once per frame
    void Update()
    {
        currentAggroTime -= Time.deltaTime; // Decrement Aggro time
        currentAggroTime = Mathf.Clamp(currentAggroTime, 0, aggroTime); // Keeps currentAggroTime above 0 and below aggroTime
        if (currentAggroTime != 0)
        {
            isAggro = true; // Stay aggro'd as long as there is still currentAggroTime
        }
        else
        {
            isAggro = false; // Stop aggroing once time is up
        }
    }
}
