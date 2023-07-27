using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

// @author: CH
// @date last updtaed: 19.07.23
// version: 1.0

public class CardUserPref : MonoBehaviour
{
    private const string DeckKey = "Deck"; // for saving data
    public List<string> deck = new List<string>(); // user saved deck
    private const string SpecialCardKey = "SpecialCards"; // for saving special cards remaining
    public List<string> specialCards = new List<string>(); // all special cards

    // new dictionary to map card names to GameObjects
    public Dictionary<string, GameObject> cardDictionary = new Dictionary<string, GameObject>();

    // all cards
    public GameObject Card01, Card02, Card03, Card04, Card05, Card06, Card07, Card08, Card09, Card10, Card11, Card12, Card13;

    private static CardUserPref instance;

    private void Awake()
    {
        // check if instance already exists
        //if (instance == null)
        //{
            // if not, make this the instance
            //instance = this;

            // to make the object persistent across scenes fr user saving and loading in next levels
            DontDestroyOnLoad(gameObject);

            // load or reset the deck and special cards depending on user progress
            LoadDeck();
            if (deck.Count < 10)
            {
                ResetSpecialCards();

            }

            // populate the card dictionary for saving/retrieval
            PopulateCardDictionary();

        //}
        //else
        //{
            // if an instance already exists, destroy it
            //Destroy(gameObject);
        //}
    }


    // save deck on user exit
    private void OnDestroy()
    {
        SaveDeck();
    }

    // retrieve user deck
    public List<string> GetDeck()
    {
        return deck;
    }

    // add card to deck, for adding special cards won
    public void AddCardToDeck(string cardName)
    {
        deck.Add(cardName);
    }

    // reset specials to contain all special cards - for new games/game over
    public void ResetSpecialCards()
    {
        specialCards.Clear();
        specialCards.AddRange(new List<string>
        {
            "Card10", "Card11", "Card12", "Card13"
        });
    }

    // for removing cards from deck easily
    public void RemoveCardFromDeck(string cardName)
    {
        deck.Remove(cardName);
    }

    // save the user deck as string
    public void SaveDeck()
    {
        string serializedDeck = SerializeDeck(deck);
        PlayerPrefs.SetString(DeckKey, serializedDeck);

        string serializedSpecialCards = SerializeDeck(specialCards);
        PlayerPrefs.SetString(SpecialCardKey, serializedSpecialCards);
    }

    // retrieve user deck
    public void LoadDeck()
    {
        string savedDeck = PlayerPrefs.GetString(DeckKey);
        string savedSpecials = PlayerPrefs.GetString(SpecialCardKey);

        // if there's a saved deck
        if (!string.IsNullOrEmpty(savedDeck))
        {
            deck = DeserializeDeck(savedDeck);
        }
        else //if not, make a new one and save it (for first gameplay)
        {
            ResetDeckToOriginal(); // reset the deck to original cards
            SaveDeck(); // save the reset deck
        }

        if (!string.IsNullOrEmpty(savedSpecials))
        {
            specialCards = DeserializeDeck(savedSpecials);
        }
        else
        {
            ResetSpecialCards();
            SaveDeck();
        }

    }

    // reset deck to original 9 cards and call method to reset specials - for new game/game over
    public void ResetDeckToOriginal()
    {
        deck.Clear();
        deck.AddRange(new List<string>
        {
            "Card01", "Card02", "Card03", "Card04", "Card05", "Card06", "Card07", "Card08", "Card09"
        });
        ResetSpecialCards();
        SaveDeck();
    }

    // JSON used to save the deck
    private string SerializeDeck(List<string> deckToSerialize)
    {
        string serializedDeck = JsonConvert.SerializeObject(deckToSerialize, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
        return serializedDeck;
    }

    // for deck retrieval
    private List<string> DeserializeDeck(string serializedDeck)
    {
        List<string> deckToDeserialize = JsonConvert.DeserializeObject<List<string>>(serializedDeck);
        return deckToDeserialize;
    }

    // retrieve cards by name string
    public GameObject GetCardByName(string cardName)
    {
        GameObject card;
        if (cardDictionary.TryGetValue(cardName, out card))
        {
            return card;
        }
        return null;
    }

    // populate the card dictionary with card names and corresponding card gameobjects
    private void PopulateCardDictionary()
    {
        cardDictionary.Clear();
        cardDictionary.Add("Card01", Card01);
        cardDictionary.Add("Card02", Card02);
        cardDictionary.Add("Card03", Card03);
        cardDictionary.Add("Card04", Card04);
        cardDictionary.Add("Card05", Card05);
        cardDictionary.Add("Card06", Card06);
        cardDictionary.Add("Card07", Card07);
        cardDictionary.Add("Card08", Card08);
        cardDictionary.Add("Card09", Card09);
        cardDictionary.Add("Card10", Card10);
        cardDictionary.Add("Card11", Card11);
        cardDictionary.Add("Card12", Card12);
        cardDictionary.Add("Card13", Card13);
    }
}
