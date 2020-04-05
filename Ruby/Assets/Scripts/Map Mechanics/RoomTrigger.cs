using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    Collider2D roomCollider;
    GameObject player;
    bool playerInRoom = false; // Is the player in the room
    public List<GameObject> activateOnEnter; // Objects to be activated when the player enters
    bool activated = false; // Have the objects been activated already
    public List<GameObject> deactivateOnExit; // Objects to be deactivated on exit
    bool deactivated = false; // Have the objects already been deactivated
    void Start()
    {
        roomCollider = GetComponent<Collider2D>(); // Get the collider for the room
        player = GameObject.FindObjectOfType<Movement>().gameObject; // Get the player
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals(player.name)) // If collision is with player
        {
            playerInRoom = true;
            if (activateOnEnter.Count > 0 && !activated) // If there are things to be activated
            {
                foreach (GameObject gameObject in activateOnEnter) // Activate them
                {
                    gameObject.SetActive(true);
                }
                activated = true; 
            }
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.Equals(player))
        {
            playerInRoom = false;
            if (deactivateOnExit.Count > 0 && deactivated)
            {
                foreach (GameObject gameObject in deactivateOnExit)
                {
                    gameObject.SetActive(false);
                }
                deactivated = false;
            }
        }

    }

    public bool IsPlayerInRoom()
    {
        return playerInRoom;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
