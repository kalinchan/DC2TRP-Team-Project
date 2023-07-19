// LevelLoad.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        handSize = 0;
    }

    private void Start()
    {
        backgroundParent = GameObject.Find("Background");

        // Set the background for the current level
        Scene currentScene = SceneManager.GetActiveScene();
        int backgroundInt = currentScene.buildIndex - 3;
        backgroundParent.transform.GetChild(backgroundInt).gameObject.SetActive(true);

        // Reset availableCards list to the deck in CardUserPref
        cardUserPref.LoadDeck();
        availableCards = new List<string>(cardUserPref.GetDeck());

        // Only 5 cards in hand at a time - 5 each turn
        maxHandSize = 5;
        cardsToDeal = maxHandSize;
        handSize = 0;

        // Deal cards
        initialiseHand();
        Debug.Log("Available cards: " + availableCards.Count);
    }

    // Deal cards
    public void initialiseHand()
    {
        CalculateNoCardsToDeal();

        for (int i = 0; i < cardsToDeal; i++)
        {
            int randomIndex = Random.Range(0, availableCards.Count);
            string cardName = availableCards[randomIndex];

            GameObject playerCard = cardUserPref.GetCardByName(cardName);
            GameObject instantiatedCard = Instantiate(playerCard, Vector3.zero, Quaternion.identity);
            instantiatedCard.transform.SetParent(PlayerArea.transform, false);
            handSize++;
        }

        Debug.Log("Available cards: " + availableCards.Count);
    }

    // Reduce hand size by 1 - called by other methods when a card is used / disposed of
    public void reduceHandSize()
    {
        handSize--;
        Debug.Log("HandSize = " + handSize);
    }

    // Calculate the number of cards needed to deal
    private void CalculateNoCardsToDeal()
    {
        cardsToDeal = maxHandSize - handSize;
    }

    public void DisplayCardAtEnd()
    {
        if (SpecialCardArea != null && cardUserPref.specialCards.Count > 0)
        {
            int specialInt = Random.Range(0, cardUserPref.specialCards.Count);

            string specialCardName = cardUserPref.specialCards[specialInt];
            GameObject specialCard = cardUserPref.GetCardByName(specialCardName);

            if (specialCard != null)
            {
                GameObject instantiatedCard = Instantiate(specialCard, Vector3.zero, Quaternion.identity);
                instantiatedCard.transform.SetParent(SpecialCardArea.transform, false);

                cardUserPref.specialCards.RemoveAt(specialInt);
                cardUserPref.AddCardToDeck(specialCardName);
                cardUserPref.SaveDeck();

                Debug.Log("Deck cards: " + cardUserPref.deck.Count);
            }
            else
            {
                Debug.LogError("Special card not found: " + specialCardName);
            }
        }
        else
        {
            Debug.LogError("SpecialCardArea is not assigned or specialCards list is empty!");
        }
    }

}
