using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Send : MonoBehaviour
{
    public TriggerReceiver triggerReceiver;
    Health hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = gameObject.GetComponent<Health>();
    }

    public void trigger()
    {
        triggerReceiver.setTrigger(true);
    }

    public void deTrigger()
    {
        triggerReceiver.setTrigger(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (hp.IsDead())
        {
            print("memes");
            trigger();
        }
    }
}
