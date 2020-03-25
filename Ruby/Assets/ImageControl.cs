using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ImageControl : MonoBehaviour
{
    private Image image;
    private ApplyBuff powerup;
    private ApplyBuff.BuffType powerupType;
    public Sprite rapid;
    public Sprite multi;
    public Sprite nothing;

    // Start is called before the first frame update
    void Awake()
    {
        powerup = GameObject.Find("Ruby").GetComponent<ApplyBuff>();
        image = GetComponent<Image>();
    }

    public void ChangeImage()
    {
        if (powerup.currentPower.Equals(ApplyBuff.BuffType.RapidFire))
        {
            image.sprite = rapid;
        }

        if (powerup.currentPower.Equals(ApplyBuff.BuffType.MultiAttack))
        {
            image.sprite = multi;
        }

        if (powerup.currentPower.Equals(ApplyBuff.BuffType.Default))
        {
            image.sprite = nothing;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
