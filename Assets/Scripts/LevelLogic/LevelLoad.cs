using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// @author: CH
// @date last updated: 19.07.23
// version: 2.0

public class LevelLoad : MonoBehaviour
{
    public GameObject PlayerArea;
    public GameObject SpecialCardArea;
    public int maxHandSize;
    public int handSize;
    public CardUserPref cardUserPref;
    public List<string> availableCards;
    public int cardsToDeal;
    public int cardsAtEnd = 1; // Number of cards displayed at the end of the level
    public Sprite Background01, Background02, Background03, Background04, Background05;
    public GameObject backgroundParent;

    private void Awake()
    {
        handSize = 0; // no cards in hand before load
}

    private void Start()
    {
        backgroundParent = GameObject.Find("Background");
        cardUserPref = GameObject.Find("Progress").GetComponent<CardUserPref>();
       

        // set the background for the current level
        Scene currentScene = SceneManager.GetActiveScene();
        int backgroundInt = currentScene.buildIndex - 3;
        backgroundParent.transform.GetChild(backgroundInt).gameObject.SetActive(true);

        // set availableCards list to the saved deck in CardUserPref script
        cardUserPref.LoadDeck();
        availableCards = new List<string>(cardUserPref.GetDeck());

        // only 5 cards in hand at a time - 5 each turn
        maxHandSize = 5;
        cardsToDeal = maxHandSize;
        handSize = 0;

        // deal cards
        initialiseHand();
        Debug.Log("Available cards: " + availableCards.Count); // checking available cards each level - debugging
        Debug.Log("Deck cards: " + cardUserPref.deck.Count); // checking deck is saved on new load - debugging
        Debug.Log("Special cards: " + cardUserPref.specialCards.Count);

    }

    // deal cards
    public void initialiseHand()
    {
        CalculateNoCardsToDeal(); // depending on cards left in hand after a turn
        Debug.Log("Available cards: " + availableCards.Count); // debugging

        for (int i = 0; i < cardsToDeal; i++)
        {
            // randomly select cards
            int randomIndex = Random.Range(0, availableCards.Count);
            string cardName = availableCards[randomIndex];

            // display in hand on screen
            GameObject playerCard = cardUserPref.GetCardByName(cardName);
            GameObject instantiatedCard = Instantiate(playerCard, Vector3.zero, Quaternion.identity);
            instantiatedCard.transform.SetParent(PlayerArea.transform, false);

            // increase handsize for cardstdeal calculation
            handSize++;
        }

        
    }


    // reduce hand size by 1 - called by other methods when a card is used / disposed of
    public void reduceHandSize()
    {
        handSize--;
        Debug.Log("HandSize = " + handSize); // debugging
    }

    // calculate the number of cards needed to deal
    private void CalculateNoCardsToDeal()
    {
        // max hand size - cards in current hand
        cardsToDeal = maxHandSize - handSize;
    }

    // display special card won at end of level
    public void DisplayCardAtEnd()
    {
        SpecialCardArea = GameObject.Find("SpecialCardHand");
        if (SpecialCardArea != null) // error handling
        {
            if (cardUserPref.specialCards.Count > 0) // error handling
            {
                // select special card at random
                int specialInt = Random.Range(0, cardUserPref.specialCards.Count);
                // get the special card
                string specialCardName = cardUserPref.specialCards[specialInt];
                GameObject specialCard = cardUserPref.GetCardByName(specialCardName);

                if (specialCard != null) // error handling
                {
                    // show the selected special card on screen to show the user what they won
                    GameObject instantiatedCard = Instantiate(specialCard, Vector3.zero, Quaternion.identity);
                    instantiatedCard.transform.SetParent(SpecialCardArea.transform, false);

                    // remove from the available special cards list in CardUserPref script
                    cardUserPref.specialCards.RemoveAt(specialInt);

                    // add the special card to the user's deck
                    cardUserPref.AddCardToDeck(specialCardName);

                    // save the deck
                    cardUserPref.SaveDeck();

                    Debug.Log("Deck cards: " + cardUserPref.deck.Count); // debugging to check added successfully
                }
                else
                {
                    Debug.LogError("Special card not found: " + specialCardName); // debugging for out of range exceptions
                }
            }
            else
            {
                Debug.LogError("specialCards list is empty!"); // debugging for out of range exceptions
            }
        }
        else
        {
            Debug.LogError("SpecialCardArea is not assigned!"); // debugging for out of range exceptions
        }
    }

}
