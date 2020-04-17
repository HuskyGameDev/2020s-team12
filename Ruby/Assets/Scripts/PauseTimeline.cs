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
    public TextAsset script;

    Sprite[] portraits;
    string[] lines;
    int currentLine = 0;
    string lineToWrite;
    float[] timerPerChar = { 0f, 0.0125f };

    void OnEnable()
    {
        if(lines == null)
        {
            ParseDialogue();
        }
        if (portraits == null)
        {
            portraits = Resources.LoadAll<Sprite>("Portrait Sprites");
        }
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);

        NextLine();
    }

    // Update is called once per frame
    void Update()
    {
        bool effectDone = TypewriterEffect();

        if(Input.GetButtonDown("Submit") || Input.GetButtonDown("Shoot"))
        {
            if(effectDone)
            {
                director.playableGraph.GetRootPlayable(0).SetSpeed(1);
            }
            else
            {
                dialogueText.text = lineToWrite;
            }
        }
    }

    void ParseDialogue()
    {
        string text = script.text;
        lines = text.Split('\n');
    }

    /// <returns>Whether or not the effect is done</returns>
    bool TypewriterEffect()
    {
        if (lineToWrite != null)
        {
            if (lineToWrite.Length > dialogueText.text.Length)
            {
                if (timerPerChar[0] >= timerPerChar[1])
                {
                    char next = lineToWrite[dialogueText.text.Length];
                    dialogueText.text += next;
                    timerPerChar[0] = 0;
                }
                else
                {
                    timerPerChar[0] += Time.deltaTime;
                }

                return false;
            }
            else
                return true;
        }
        return false;
    }

    void NextLine()
    {
        bool updatedDialogue = false;

        do
        {
            string nextLine = lines[currentLine];
            int colon = nextLine.IndexOf(':');
            if (colon < 0)
            {
                currentLine++;
                continue;
            }

            switch (nextLine.Substring(0, colon))
            {
                case "Portrait":
                    int quoteStart = nextLine.IndexOf('"') + 1;
                    string portraitName = nextLine.Substring(quoteStart, nextLine.LastIndexOf('"') - quoteStart);
                    SetPortrait(portraitName);
                    break;
                case "Speaker":
                    int speakerStart = nextLine.IndexOf('"') + 1;
                    string speakName = nextLine.Substring(speakerStart, nextLine.LastIndexOf('"') - speakerStart);
                    speakerName.text = speakName;
                    break;
                case "Dialogue":
                    int dialogueStart = nextLine.IndexOf('"') + 1;
                    string dialogue = nextLine.Substring(dialogueStart, nextLine.LastIndexOf('"') - dialogueStart);
                    lineToWrite = dialogue;
                    dialogueText.text = "";
                    updatedDialogue = true;
                    break;
                default:
                    print("oH NOOOOO");
                    currentLine++;
                    return;
            }

            currentLine++;
        }
        while (!updatedDialogue);
    }

    void SetPortrait(string name)
    {
        foreach(Sprite s in portraits)
        {
            if(s.name.Equals(name))
            {
                portraitSprite.sprite = s;
                return;
            }
        }
    }
}
