using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelLoad : MonoBehaviour
{
    public GameObject Card01, Card02, Card03, Card04, Card05, Card06, Card07, Card08, Card09, Card10, Card11, Card12;
    public GameObject PlayerArea;
    public int maxHandSize, cardsToDeal;
    public int handSize;
    public GameObject dCButton;
    public Sprite Background01, Background02, Background03;
    public GameObject backgroundParent;
    public int specialInt;
 

    public List<GameObject> cards = new List<GameObject>();
    public List<GameObject> specialcards = new List<GameObject>();
    List<Sprite> backgrounds = new List<Sprite>();

    void Awake()
    {
        handSize = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        // ensure only 5 cards added to hand
        specialInt = 0;
        maxHandSize = 5;
       
        // start level needing 5 cards to deal
        cardsToDeal = maxHandSize;

        specialcards.AddRange(new List<GameObject>
        {
            Card10, Card11, Card12
        }
        );

        // get all cards
        // if level = 1
        cards.AddRange(new List<GameObject>
            {
                Card01, Card02, Card03, Card04, Card05, Card06, Card07, Card08, Card09
        });
   

        initialiseHand();

        // load 5 cards into hand

        //change background
        backgrounds.AddRange(new List<Sprite>
            {
                Background01, Background02, Background03
        }
        );
        int randomIndex = Random.Range(0, backgrounds.Count);

        // Enable the background image at the random index
        backgroundParent.transform.GetChild(randomIndex).gameObject.SetActive(true);


    }

   

    // load 5 cards into hand
    public void initialiseHand()
    {
        // load 5 cards
        

        for (var i = 0; i < calculateNoCardsToDeal(); i++)
        {

            GameObject playerCard = Instantiate(cards[Random.Range(0, cards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.transform.SetParent(PlayerArea.transform, false);
            Debug.Log("cards range: " + cards.Count);
            
        }
        handSize = maxHandSize;
        Debug.Log("HandSize = " + handSize);

    }

    // reduce no of cards in hand when played
    public void reduceHandSize()
    {
        handSize--;
        Debug.Log("HandSize = "+ handSize);
    }

    public int calculateNoCardsToDeal()
    {
        cardsToDeal = maxHandSize - handSize;
        return cardsToDeal;
    }

    public void addSpecial()
    {
        cards.Add(specialcards[specialInt]);
        if (specialInt == 2)
        {
            specialInt = 0;
        }
        else { specialInt++; }
    }



}

