using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool gamePaused = false;

    public GameObject pauseMenuUI;

    void Start()
    {
        pauseMenuUI.SetActive(false); // To not start paused
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);

        Time.timeScale = 1f;

        gamePaused = false; // Set game to resume

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;

        gamePaused = true; // Set game to be paused

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
