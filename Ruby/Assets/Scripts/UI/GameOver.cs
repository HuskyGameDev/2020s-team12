using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public GameObject gameOverUI;
    Transform player;

    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("MapA") && player == null)
        {
            player = GameObject.Find("Trevor").transform; // Sets Trevor as the player if in the MapA scene
        }
        else if (player == null)
        {
            player = GameObject.Find("Ruby").transform; // Sets Ruby
        }
        gameOverUI.SetActive(false); // To not start in game over
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) // If it can't find the player
        {
            gameOverUI.SetActive(true); // Pulls up the game over screen

        }
    }

    public void LoadTitleScreen()
    {
        SceneManager.LoadScene("Title Screen"); // Loads the title screen, so long as the name doesn't change
    }

}
