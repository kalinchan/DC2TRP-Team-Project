using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Version 1.0 / Date: 10/01/2023 / Author CH

// Script for initialising the deck of cards - CH
public class Deck : MonoBehaviour {

<<<<<<< Updated upstream
    int deckHandSize; // starting number of cards in hand
    int deckSize; // totalDeckSize minus special cards


    List<Card> deckNoSpecial = new List<Card>(); // deck minus special cards (used by player and enemy - unless player wins special)
=======
    public int deckHandSize; // starting number of cards in hand
    public int deckSize; // totalDeckSize minus special cards
    public int x;

    public List<Card> deckNoSpecials = new List<Card>(); // deck minus special cards (used by player and enemy - unless player wins special)
    public List<Card> container = new List<Card>(); // for shuffling
    public List<Card> staticDeck = new List<Card>();

    public GameObject Hand;
    public GameObject CardToHand;
    public GameObject[] Clones;
    public GameObject PlayerArea;
>>>>>>> Stashed changes

    // Start is called before the first frame update - initialise - CH
    public void Start()
    {
        x = 0;
        deckHandSize = 5;
<<<<<<< Updated upstream
        InitialiseDeckNoSpecial();
    }

    // full deck of cards (depending on rarity) - rarity can be found in CardDataBase.cs - CH

        // rarity = 1 NOT to be in hand unless obtained through win (Special1,2,3)
        // rarity = 2 rare (Defence2, Defence3)
        // rarity = 3 uncommon (Attack3, Defence 1)
        // rarity = 4 common (Attack1, Attack2)
        // total cards


    // initialise deck of 18 cards (no special cards) for level 1 hand - CH
=======
        deckSize = 30;
        InitialiseDeckNoSpecial();

        //for (int i=0; i<deckHandSize; i++)
        //{
        //    x = Random.Range(1, 5);
        //    deckNoSpecials[i] = CardDataBase.cardList[x];
            
            //GameObject CardToHand = Instantiate(deckNoSpecials.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
            //CardToHand.transform.SetParent(PlayerArea.transform, false);
        //}

    }

    void Update()
    {
        staticDeck = deckNoSpecials;
    }

    //IEnumerator deckEnum()
    //{
    //    yield return new WaitForSeconds(1);
    //    Clones = GameObject.FindGameObjectsWithTag("Clone");

    //    foreach(GameObject Clone in Clones)
    //    {
    //        Destroy(Clone);
    //    }
    //}

    //IEnumerator StartGame()
    //{
    //    for(int i = 0; i<5; i++)
    //    {
    //        yield return new WaitForSeconds(1);
    //        Instantiate(CardToHand, transform.position, transform.rotation);

            
    //    }
    //}

    public int getDeckSize()
    {
        return deckSize;
    }


    // initialise deck of 18 cards (no special cards) - CH
>>>>>>> Stashed changes
    void InitialiseDeckNoSpecial()
    {
        // TODO: initialise deck of 18 cards (no special cards) here
        x = 0;
        for (int i = 0; i < deckSize; i++)
        {
            // avoid specials (9, 10, 11)
            x = Random.Range(0, 8); 
            deckNoSpecials[i] = CardDataBase.cardList[i];

            //shuffle deck cards
            container[0] = deckNoSpecials[i];
            int randomIndex = Random.Range(i, deckSize);
            deckNoSpecials[i] = deckNoSpecials[randomIndex];
            deckNoSpecials[randomIndex] = container[0];
            //StartCoroutine(StartGame());
        }


    }

}
