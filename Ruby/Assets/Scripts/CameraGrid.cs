using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGrid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }
    void OnDrawGizmos() // A drawing utility
    {
        Vector3 room = new Vector3(30, 20, 1); // Room size
        
        Gizmos.color = Color.black; // Colors the drawing
        
        for(int i = -100; i<100; i++) // Makes many boxes at different centers of room size, can be made bigger if needed.
        {
            for(int j = -100; j<100; j++)
            {
                Vector3 center = new Vector3((i*30), ((j*20)-1), 0); // Makes new center

                Gizmos.DrawWireCube(center, room); // Draws a cube
            }
        }
    }

// Update is called once per frame
void Update()
    {
        
    }
}
