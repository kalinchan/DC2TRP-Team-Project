using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Version 1.0 / Date: 10/01/2023 / Author CH

// script to enlarge card when hovering - CH
public class CardZoom : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject Card;
    Vector2 temp;

    public void Awake()
    {
        Canvas = GameObject.Find("Background");
    }

    // hover method when mouse over - CH
    public void Hover()
    {
        temp = Card.transform.localScale;

        temp.x = temp.x * 1.5f;
        temp.y = temp.y * 1.5f;

        Card.transform.localScale = temp;
        Card.transform.Translate(0, 100, 0);
        Card.layer = LayerMask.NameToLayer("Zoom"); // to layer above cards
    }

    // return to original size when mouse exit - CH
    public void HoverExit()
    {
        Card.transform.localScale = new Vector2(1, 1);
        Card.transform.Translate(0, -100, 0);
        Card.layer = LayerMask.NameToLayer("Cards");
    }

}
