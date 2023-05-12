using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
// Version 1.0 / Date: 09/01/2023 / Author CH

// script for This Card - references Card script
// used to obtain data using card id and apply it to Card prefab object, plus alter some features
public class ThisCard : MonoBehaviour
{
    public List<Card> thisCard = new List<Card>();
    public int thisId; // use this id to get info with corresponding card id

    public int id;
    public string cardName;
    public int energyCost;
    public string group;
    public int rarity;
    public int damage;
    public int defence;
    public string cardDescription;

    public Sprite thisCardSprite;
    public Image spriteImage;
    public Image imgFrame;
    public Image cardFrame;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI energyCostText;
    public TextMeshProUGUI descriptionText;

    // Start is called before the first frame update
    void Start()
    {
        thisCard[0] = CardDataBase.cardList[thisId];
        thisCardSprite = thisCard[0].cardSprite;

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
            imgFrame.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            cardFrame.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        }
        if (thisCard[0].group == "Defence")
        {
            imgFrame.GetComponent<Image>().color = new Color32(18, 81, 243, 255);
            cardFrame.GetComponent<Image>().color = new Color32(18, 81, 243, 255);
        }
        if (thisCard[0].group == "Special")
        {
            imgFrame.GetComponent<Image>().color = new Color32(0, 255, 9, 255);
            cardFrame.GetComponent<Image>().color = new Color32(0, 255, 9, 255);
        }

    }
}
