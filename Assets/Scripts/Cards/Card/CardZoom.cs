//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.UI;
//Version 1.0 / Date: 10 / 01 / 2023 / Author CH

//    script to enlarge card when hovering - CH
//public class CardZoom : MonoBehaviour
//{

//    private Image img, activeImg;
//    public Canvas hand, active;
//    public GameObject ZoomCard;
//    private GameObject zoomCard;
//    private Sprite zoomSprite;
//    public GameObject Canvas;

//    public void Awake()
//    {
//        hand = GameObject.Find("Background").GetComponent<Canvas>();
//        img = GetComponent<Image>();
//        activeImg = GameObject.Find("ActiveCard").GetComponent<Image>();
//        active = GameObject.Find("ActiveArea").GetComponent<Canvas>();
//        Canvas = GameObject.Find("Main Canvas");
//    }

//    public void Hover()         // commented out for hack purposes
//    {
//        Sets the activeCanvas to be the top canvas in terms of sorting order
//        active.sortingOrder = 1;
//        Sets the activeCard image to be the image of the card we are hovered on
//        activeImg.sprite = img.sprite;
//        Sets the alpha of the activeCard to be 255(Completely opaque)
//        var tempColor = activeImg.color;
//        tempColor.a = 255f;
//        activeImg.color = tempColor;
//        Sets the x co-ordinate of the activeCard to be the x co-ordinate of the card that we are hovered on
//        activeImg.transform.position = new Vector3(img.transform.position.x, activeImg.transform.position.y, activeImg.transform.position.z);
//        Sets the scale to be 1.5x bigger(To highlight the card we are hovered on)
//        activeCardImage.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

//    }

//    public void HoverExit()
//    {
//        Sets the activeCanvas to be behind the player hand again
//        active.sortingOrder = -1;
//        Sets the alpha of the activeCard to 0(Completely transparent)
//        var tempColor = activeImg.color;
//        tempColor.a = 0f;
//        activeImg.color = tempColor;
//        Sets the scale of the card to 1
//        activeCardImage.transform.localScale = new Vector3(1f, 1f, 1f);
//    }

//    hover method when mouse over - CH
//    public void Hover()
//    {
//        zoomCard = Instantiate(ZoomCard, new Vector2(Input.mousePosition.x, Input.mousePosition.y + 150), Quaternion.identity);
//        zoomCard.GetComponent<Image>().sprite = zoomSprite;
//        zoomCard.transform.SetParent(Canvas.transform, true);
//        RectTransform rect = zoomCard.GetComponent<RectTransform>();
//        rect.sizeDelta = new Vector3(1f, 1f, 1f);
//    }

//     return to original size when mouse exit - CH
//    public void HoverExit()
//    {
//        Destroy(zoomCard);
//    }

//    public void changeParent(GameObject parent)
//    {
//        gameObject.transform.SetParent(parent.transform, true);
//    }

//}
