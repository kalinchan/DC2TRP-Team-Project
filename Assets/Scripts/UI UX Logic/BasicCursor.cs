using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCursor : MonoBehaviour
{
    // for menus, buttons, EOL buttons, level-select, etcetcetc...


    // cursor alteration
    public Texture2D cursorArrow; //default pointer
    public Texture2D cursorHand; //interactable
    public CursorMode cursorMode;


    public void OnMouseOver() {
        Cursor.SetCursor(cursorHand, Vector2.zero, cursorMode);
    }

    public void OnMouseOut() {
        Cursor.SetCursor(cursorArrow, Vector2.zero, cursorMode);
    }


}
