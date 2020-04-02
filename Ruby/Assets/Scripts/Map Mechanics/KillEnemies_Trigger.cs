using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemies_Trigger : MonoBehaviour
{
    bool triggered = false;
    public List<GameObject> enemyList;
    // Start is called before the first frame update
    void Start()
    {

    }

    public bool isTriggered()
    {
        return triggered;
    }

    public void setTrigger(bool value)
    {
        triggered = value;
    }

    bool CheckIfEverythingsDead()
    {
        bool everythingsDead = true;
        foreach (GameObject enemy in enemyList)
        {
            if (enemy != null)
            {
                everythingsDead = false;
            }
        }
        return everythingsDead;
    }
    // Update is called once per frame
    void Update()
    {
        if (CheckIfEverythingsDead())
        {
            Destroy(gameObject);
        }
    }
}
