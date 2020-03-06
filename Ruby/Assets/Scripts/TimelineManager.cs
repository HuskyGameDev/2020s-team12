using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    private bool fix = false;
    public Animator rubyAnimator;
    RuntimeAnimatorController rubyAnim;
    public PlayableDirector director;

    void OnEnable()
    {
        rubyAnim = rubyAnimator.runtimeAnimatorController;
        rubyAnimator.runtimeAnimatorController = null;
    }

    // Update is called once per frame
    void Update()
    {

        if(director.state != PlayState.Playing && !fix)
        {
            fix = true;
            rubyAnimator.runtimeAnimatorController = rubyAnim;
        }
    }
}
