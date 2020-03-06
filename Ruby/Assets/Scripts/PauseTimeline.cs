using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PauseTimeline : MonoBehaviour
{
    public PlayableDirector director;
    public Text dialogueText;
    public Text speakerName;
    public Image portraitSprite;
    public Image dialogBox;

    public Sprite[] portraits;

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
            portraitSprite.sprite = portraits[0];
        }
    }
}
