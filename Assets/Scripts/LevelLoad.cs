using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelLoad : MonoBehaviour
{
    public GameObject Card01, Card02, Card03, Card04, Card05, Card06, Card07, Card08, Card09;
    public GameObject PlayerArea;
    public int maxHandSize, cardsToDeal;
    public int handSize;
    public GameObject dCButton;

    public List<GameObject> cards = new List<GameObject>();

    void Awake()
    {
        handSize = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        // ensure only 5 cards added to hand
        
        maxHandSize = 5;
       
        // start level needing 5 cards to deal
        cardsToDeal = maxHandSize;
        

        // get all cards
        cards.AddRange(new List<GameObject>
            {
                Card01, Card02, Card03, Card04, Card05, Card06, Card07, Card08, Card09
        }
        );


        // load 5 cards into hand

    }

    // load 5 cards into hand
    public void initialiseHand()
    {
        // load 5 cards
        

        for (var i = 0; i < calculateNoCardsToDeal(); i++)
        {
            GameObject playerCard = Instantiate(cards[Random.Range(0, cards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.transform.SetParent(PlayerArea.transform, false); 
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



}

