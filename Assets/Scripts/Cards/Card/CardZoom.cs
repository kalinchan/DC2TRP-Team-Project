using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
// Version 1.0 / Date: 10/01/2023 / Author CH

// script to enlarge card when hovering - CH
public class CardZoom : MonoBehaviour
{
    
    private Image img, activeImg;
    public Canvas hand, active;

    public void Awake()
    {
        hand = GameObject.Find("Background").GetComponent<Canvas>();
        img = GetComponent<Image>();
        activeImg = GameObject.Find("ActiveCard").GetComponent<Image>();
        active = GameObject.Find("ActiveArea").GetComponent<Canvas>();
    }

    public void Hover()
    {
        //Sets the activeCanvas to be the top canvas in terms of sorting order
        active.sortingOrder = 1;
        //Sets the activeCard image to be the image of the card we are hovered on
        activeImg.sprite = img.sprite;
        //Sets the alpha of the activeCard to be 255 (Completely opaque)
        var tempColor = activeImg.color;
        tempColor.a = 255f;
        activeImg.color = tempColor;
        //Sets the x co-ordinate of the activeCard to be the x co-ordinate of the card that we are hovered on
        activeImg.transform.position = new Vector3(img.transform.position.x, activeImg.transform.position.y, activeImg.transform.position.z);
        //Sets the scale to be 1.5x bigger (To highlight the card we are hovered on)
        //activeCardImage.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void HoverExit()
    {
        //Sets the activeCanvas to be behind the player hand again
        active.sortingOrder = -1;
        //Sets the alpha of the activeCard to 0 (Completely transparent)
        var tempColor = activeImg.color;
        tempColor.a = 0f;
        activeImg.color = tempColor;
        //Sets the scale of the card to 1
        //activeCardImage.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    // hover method when mouse over - CH
    //public void Hover()
    //{
    //    Debug.Log("ENTER " + name);
    //    img.canvas.overrideSorting = true;
    //    img.transform.SetParent(active.transform);
    //    active.sortingOrder= 1;
    //    //temp = img.transform.localScale;

    //    //temp.x = temp.x * 1.5f;
    //    //temp.y = temp.y * 1.5f;

    //    //img.transform.localScale = temp;

    //    //img.transform.Translate(0, 100, 0);
    //    //img.layer = LayerMask.NameToLayer("Zoom"); // to layer above cards
    //}

    //// return to original size when mouse exit - CH
    //public void HoverExit()
    //{
    //    Debug.Log("EXIT " + name);
    //    img.canvas.overrideSorting = false;
    //    img.transform.SetParent(hand.transform);
    //    active.sortingOrder = -1;
    //    //changeParent(Canvas);
    //    //img.transform.localScale = new Vector2(1, 1);
    //    //img.transform.Translate(0, -100, 0);
    //    //Card.layer = LayerMask.NameToLayer("Cards");
    //}

    //public void changeParent(GameObject parent)
    //{
    //    gameObject.transform.SetParent(parent.transform, true);
    //}

}
