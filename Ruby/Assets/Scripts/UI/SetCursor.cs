using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetCursor : MonoBehaviour
{
    public static Texture2D shootReticle;
    public static Texture2D normalCursor;
    public static Vector2 RETICLE_HOTSPOT = new Vector2(16, 16);

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

        if (SceneManager.GetActiveScene().name.Equals("Title Screen"))
        {
            Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(shootReticle, RETICLE_HOTSPOT, CursorMode.Auto);
        }
    }

    public static void ChangeCursor(CursorType cursorType)
    {
        switch (cursorType)
        {
            case CursorType.shoot:
                Cursor.SetCursor(SetCursor.shootReticle,RETICLE_HOTSPOT,CursorMode.Auto);
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
