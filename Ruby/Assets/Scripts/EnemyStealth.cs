using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStealth : MonoBehaviour
{
    public float initOpacity = 0; // Initial opacity of the sprite from 0 (invisible) to 1 (opaque)
    public float aggroOpacity = 1; // Opacity of sprite during aggro
    private Color initColor;
    private Color aggroColor;
    SpriteRenderer enemySprite;
    AggroTimer aggroTimer;

    // Start is called before the first frame update
    void Start()
    {
        enemySprite = GetComponent<SpriteRenderer>();
        aggroTimer = GetComponent<AggroTimer>();

        initOpacity = Mathf.Clamp(initOpacity, 0, 1);
        aggroOpacity = Mathf.Clamp(aggroOpacity, 0, 1);

        initColor = enemySprite.color;
        initColor.a = initOpacity;
        aggroColor = enemySprite.color;
        aggroColor.a = aggroOpacity;
        enemySprite.color = initColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (aggroTimer.isAggro)
        {
            enemySprite.color = aggroColor;
        }
        else
        {
            enemySprite.color = initColor;
        }
    }
}
