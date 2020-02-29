using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGrid : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
       
    }
    void OnDrawGizmos()
    {
        Vector3 room = new Vector3(30, 20, 1);
        
        Gizmos.color = Color.black;
        
        for(int i = -100; i<100; i++)
        {
            for(int j = -100; j<100; j++)
            {
                Vector3 center = new Vector3((i*30), ((j*20)-1), 0);
                Gizmos.DrawWireCube(center, room);
            }
        }
    }

// Update is called once per frame
void Update()
    {
        
    }
}
