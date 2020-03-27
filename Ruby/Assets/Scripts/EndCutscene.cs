using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCutscene : MonoBehaviour
{

    // Start is called before the first frame update
    void OnEnable()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Set in build settings. Check index.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
