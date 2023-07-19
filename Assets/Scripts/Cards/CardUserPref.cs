// CardUserPref.cs
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public class CardUserPref : MonoBehaviour
{
    private const string DeckKey = "Deck";
    public List<string> deck = new List<string>();
    public List<string> specialCards = new List<string>();

    // All cards
    public Dictionary<string, GameObject> cardDictionary = new Dictionary<string, GameObject>(); // New dictionary to map card names to GameObjects

    public GameObject Card01, Card02, Card03, Card04, Card05, Card06, Card07, Card08, Card09, Card10, Card11, Card12, Card13;

    public GameObject SpecialCardArea;

    private static CardUserPref instance;

    private void Awake()
    {
        // Check if an instance already exists
        if (instance == null)
        {
            // If not, make this the instance
            instance = this;

            // Make the object persistent across scenes
            DontDestroyOnLoad(gameObject);

            // Load or reset the deck and special cards
            LoadDeck();
            ResetDeckToOriginal();

            // Populate the card dictionary
            PopulateCardDictionary();
        }
        else
        {
            // If an instance already exists, destroy this object
            Destroy(gameObject);
        }
    }


    private void OnDestroy()
    {
        SaveDeck();
    }

    public List<string> GetDeck()
    {
        return deck;
    }

    public void AddCardToDeck(string cardName)
    {
        deck.Add(cardName);
    }

    public void ResetSpecialCards()
    {
        specialCards.Clear();
        specialCards.AddRange(new List<string>
        {
            "Card10", "Card11", "Card12", "Card13"
        });
    }

    public void RemoveCardFromDeck(string cardName)
    {
        deck.Remove(cardName);
    }

    public void SaveDeck()
    {
        string serializedDeck = SerializeDeck(deck);
        PlayerPrefs.SetString(DeckKey, serializedDeck);
    }

    public void LoadDeck()
    {
        string savedDeck = PlayerPrefs.GetString(DeckKey);
        if (!string.IsNullOrEmpty(savedDeck))
        {
            deck = DeserializeDeck(savedDeck);
        }
        else
        {
            ResetDeckToOriginal(); // Reset the deck to original cards
            SaveDeck(); // Save the reset deck
        }
    }

    public void ResetDeckToOriginal()
    {
        deck.Clear();
        deck.AddRange(new List<string>
        {
            "Card01", "Card02", "Card03", "Card04", "Card05", "Card06", "Card07", "Card08", "Card09"
        });
        SaveDeck();
        ResetSpecialCards();
    }

    private string SerializeDeck(List<string> deckToSerialize)
    {
        string serializedDeck = JsonConvert.SerializeObject(deckToSerialize, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
        return serializedDeck;
    }

    private List<string> DeserializeDeck(string serializedDeck)
    {
        List<string> deckToDeserialize = JsonConvert.DeserializeObject<List<string>>(serializedDeck);
        return deckToDeserialize;
    }

    public GameObject GetCardByName(string cardName)
    {
        GameObject card;
        if (cardDictionary.TryGetValue(cardName, out card))
        {
            return card;
        }
        return null;
    }

    // Populate the card dictionary with card names and corresponding GameObjects
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
