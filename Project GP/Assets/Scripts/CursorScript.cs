using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    // Cursor Image
    public Texture2D crosshair;

    // Position offset of cursor
    Vector2 cursorOffset;

    // Start is called before the first frame update
    void Start()
    {
        cursorOffset = new Vector2(crosshair.width / 2, crosshair.height / 2);

        // Set the cusor to the crosshair
        Cursor.SetCursor(crosshair, cursorOffset, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
