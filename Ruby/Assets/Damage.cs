using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    public float damageAmount;
    Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
