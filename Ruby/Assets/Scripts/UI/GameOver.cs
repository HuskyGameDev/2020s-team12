using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public GameObject gameOverUI;
    Transform player;
    public static bool gameOver = false;

    void Start()
    {

        player = GameObject.FindObjectOfType<Movement>().gameObject.transform;
        gameOverUI.SetActive(false); // To not start in game over
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) // If it can't find the player
        {
            gameOverUI.SetActive(true); // Pulls up the game over screen
            SetCursor.ChangeCursor(SetCursor.CursorType.normal);
            gameOver = true;
        }
    }

    public void LoadTitleScreen()
    {
        SceneManager.LoadScene("Title Screen"); // Loads the title screen, so long as the name doesn't change
    }

    public bool getGameOver()
    {
        return gameOver;
    }

}
