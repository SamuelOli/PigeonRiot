using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    //public Texture2D cursorTexture;

    void Start()
    {
        //Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        //Cursor.visible = false;
    }

   
    void Update()
    {
        //Deixa a mira na posuição do mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        transform.position = mousePosition;
    }
}
