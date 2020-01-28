using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    public float damageAmount; // Creates a Damage Amount to set in Unity
    Collider2D col; // Creates a collider

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>(); //Gets collider class as an object
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
