using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
    private GameObject turnManager;

    // cursor alteration
    public Texture2D cursorArrow; //default pointer
    public Texture2D cursorHand; //interactable
    public CursorMode cursorMode;


    // Start is called before the first frame update
    void Start()
    {
        turnManager = GameObject.Find("Turn Manager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
  
        turnManager.GetComponent<TurnManager>().switchTurn();
    }

    // change cursor on button hover to show it is interactible
    public void OnMouseEnter()
    {
        Cursor.SetCursor(cursorHand, Vector2.zero, cursorMode);
    }

    // return cursor to arrow when exit interactable object
    public void OnMouseExit()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, cursorMode);
    }
}
