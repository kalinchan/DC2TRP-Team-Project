using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// Version 1.0 / Date: 09/01/2023 / Author CH -- initial implementation of ThisCard and images
// Version 1.1 / Date: 13/05/2023 / Author CH -- adding cursor changes

// script for This Card - references Card script
// used to obtain data using card id and apply it to Card prefab object, plus alter some features
public class ThisCard : MonoBehaviour
{
    public List<Card> thisCard = new List<Card>();
    public int thisId; // use this id to get info with corresponding card id

    // card info
    public int id;
    public string cardName;
    public int energyCost;
    public string group;
    public int rarity;
    public int damage;
    public int defence;
    public string cardDescription;

    // card images
    public Sprite thisCardSprite;
    public Image spriteImage;
    public Image imgFrame;
    public Image cardFrame;
    public Image groupImage;

    // card text
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI energyCostText;
    public TextMeshProUGUI descriptionText;

    // cursor alteration
    public Texture2D cursorArrow; //default pointer
    public Texture2D cursorHand; //interactable
    public CursorMode cursorMode;

    // turn manager for cursor
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        thisCard[0] = CardDataBase.cardList[thisId];
        thisCardSprite = thisCard[0].cardSprite;
        Cursor.SetCursor(cursorArrow, Vector2.zero, cursorMode);
        player = GameObject.Find("Player");
    }

    // Update is called once per frame - update info with data from card with matching id
    void Update()
    {

        id = thisCard[0].id;
        cardName = thisCard[0].cardName;
        energyCost = thisCard[0].energyCost;
        group = thisCard[0].group;
        rarity = thisCard[0].rarity;
        damage = thisCard[0].damage;


        defence = thisCard[0].defence;
        cardDescription = thisCard[0].cardDescription;

        nameText.text = "" + cardName;
        energyCostText.text = "" + energyCost;
        descriptionText.text = "" + cardDescription;

        spriteImage.sprite = thisCardSprite;

        // change color of card frame (and sprite frame) depending on Attack(Red), Defence(Blue), or Sepcial(Green/Yellow)
        if (thisCard[0].group == "Attack")
        {
            
            cardFrame.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        }
        if (thisCard[0].group == "Defence")
        {
            
            cardFrame.GetComponent<Image>().color = new Color32(18, 81, 243, 255);
        }
        if (thisCard[0].group == "Special")
        {
            
            cardFrame.GetComponent<Image>().color = new Color32(9, 251, 52, 217);
        }

    }

    // change cursor on card hover to show it is interactible
    public void OnMouseEnter()
    {
        // if player turn
        if (player.GetComponent<PlayerLogic>().myTurn == true)
        {
            Cursor.SetCursor(cursorHand, Vector2.zero, cursorMode);
        }
        else { OnMouseExit(); }
    }

    // cursor to arrow when exit / not over interactable object
    public void OnMouseExit()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, cursorMode);
    }

    public void SetSelectedCard()
    {
        Hand hand = FindObjectOfType<Hand>();
        if (hand != null)
        {
            hand.OnCardClick(this);
            Debug.Log("card selected");
        }
        else
        {
            Debug.Log("hand is null");
        }
    }


}
