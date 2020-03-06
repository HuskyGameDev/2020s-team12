using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PauseTimeline : MonoBehaviour
{
    public PlayableDirector director;
    // Start is called before the first frame update
    void OnEnable()
    {
        director.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Submit") == 1)
        {
            director.Resume();
        }
    }
}
