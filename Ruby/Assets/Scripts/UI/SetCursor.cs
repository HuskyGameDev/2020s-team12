using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    public Texture2D shootReticle;
    public Texture2D normalCursor;
    Vector2 RETICLE_HOTSPOT = new Vector2(16, 16);

    public enum CursorType
    {
        shoot,
        normal
    }

    // Start is called before the first frame update
    void Start()
    {
        if (shootReticle == null)
        {
            shootReticle = Resources.Load("cursor_reticle") as Texture2D;
        }
        if (normalCursor == null)
        {
            normalCursor = Resources.Load("cursor") as Texture2D;
        }
        Cursor.SetCursor(shootReticle, RETICLE_HOTSPOT, CursorMode.Auto);
    }

    public void ChangeCursor(CursorType cursorType)
    {
        switch (cursorType)
        {
            case CursorType.shoot:
                Cursor.SetCursor(shootReticle,RETICLE_HOTSPOT,CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
