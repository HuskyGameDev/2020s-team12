using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOrganizer : MonoBehaviour
{
    public static List<Scene> trevorScenes = new List<Scene>();

    // Start is called before the first frame update
    void Start()
    {
        trevorScenes.Add(SceneManager.GetSceneByName("MapA"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
