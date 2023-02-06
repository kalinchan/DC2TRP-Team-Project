using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Version 1.0 / Date: 10/01/2023 / Author CH

// Script for initialising the deck of cards - CH
public class Deck : MonoBehaviour {

    int deckHandSize; // starting number of cards in hand
    int totalDeckSize; // all cards total
    int deckSize; // totalDeckSize minus special cards

    List<Card> totalDeck = new List<Card>(); // list to store total deck
    List<Card> deckNoSpecial = new List<Card>(); // deck minus special cards (used by player and enemy - unless player wins special)

    // Start is called before the first frame update - initialise - CH
    void Start()
    {
        deckHandSize = 5;
        totalDeckSize = 21; // total of all cards, see InitialiseFullDeck() comments
        deckSize = 18;
    }

    // initialise full deck of 21 cards (depending on rarity) - rarity can be found in CardDataBase.cs - CH
    void InitialiseFullDeck()
    {
        // TODO: initialise full deck of 21 cards here

        // rarity = 1 - 1 of each card - NOT to be in hand unless obtained through win (Special1,2,3)
        // rarity = 2 - 2 of each card - rare (Defence2, Defence3)
        // rarity = 3 - 3 of each card - uncommon (Attack3, Defence 1)
        // rarity = 4 - 4 of each card - common (Attack1, Attack2)
        // total: 21 cards
    }

    // initialise deck of 18 cards (no special cards) - CH
    void InitialiseDeckNoSpecial()
    {
        // TODO: initialise deck of 18 cards (no special cards) here
    }

}
