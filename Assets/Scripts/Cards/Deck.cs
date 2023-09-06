using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// @author: CH
// version: 1.0

public class Deck : MonoBehaviour
{
    public GameObject DeckArea;
    public CardUserPref cardUserPref;
    public List<string> userCards;
    public GameObject deckPanel;

    void Start()
    {
        cardUserPref = GameObject.Find("Progress").GetComponent<CardUserPref>();
        cardUserPref.LoadDeck();
        userCards = new List<string>(cardUserPref.GetDeck());
        deckPanel.SetActive(false);
    }

    public void loadDeck()
    {
        deckPanel.SetActive(true);

        for (int i = 0; i < userCards.Count; i++)
        {
            // display on screen
            string cardName = userCards[i];
            GameObject playerCard = cardUserPref.GetCardByName(cardName);
            GameObject instantiatedCard = Instantiate(playerCard, Vector3.zero, Quaternion.identity);
            instantiatedCard.transform.SetParent(DeckArea.transform, false);
        }
    }

    public void back()
    {
        deckPanel.SetActive(false);
        userCards.Clear();

    }



}