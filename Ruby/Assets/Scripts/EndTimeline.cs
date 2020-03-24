using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class EndTimeline : MonoBehaviour
{
    public GameObject skipMenuUI;

    public PlayableDirector director;

    public static bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        skipMenuUI.SetActive(false); // Doesn't start in skip
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Back();
            }
            else{

                OpenMenu();
            }
        }
    }

    public void OpenMenu()
    {
        skipMenuUI.SetActive(true);
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        isPaused = true;
    }

    public void Back()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);

        skipMenuUI.SetActive(false);

        isPaused = false;
    }

    public void End()
    {
        director.Stop();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Set in build settings. Check index.

    }
}
