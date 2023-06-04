using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{
    public GameObject Card01, Card02, Card03, Card04, Card05, Card06, Card07, Card08, Card09, Card10, Card11, Card12, Card13;
    public GameObject PlayerArea;
    public int maxHandSize, cardsToDeal;
    public int handSize;
    public GameObject dCButton;
    public Sprite Background01, Background02, Background03;
    public GameObject backgroundParent;
    public int specialInt;
    public int level;
    private LevelManager levelManager;
    public int backgroundInt;

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
        // get current scene (current level) --
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        // ensure only 5 cards added to hand
        specialInt = currentScene.buildIndex - 3;
        maxHandSize = 5;
        backgroundInt = currentScene.buildIndex - 3;
        // level 1 = build index 3, so for first item in list [0] we must -3 from the build index // level 2 = index 4 so background list item [1]

        // set backgrounds list
        backgrounds.AddRange(new List<Sprite>
            {
                Background01, Background02, Background03, Background01, Background02, Background03
        }
        );

        backgroundParent.transform.GetChild(backgroundInt).gameObject.SetActive(true);

        // start level needing 5 cards to deal
        cardsToDeal = maxHandSize;

        // cards without special for first round
        cards.AddRange(new List<GameObject>
            {
                Card01, Card02, Card03, Card04, Card05, Card06, Card07, Card08, Card09
        });

        // list of special cards to iterate through and add in next levels
        specialcards.AddRange(new List<GameObject>
        {
            Card10, Card11, Card12, Card13
        }
        );

        // if level 1 do not add the special card to player hand deck
        if (sceneName == "BattleScene")
        {
            initialiseHand();
        }
        else // if after first level, continue with adding special cards that have been won...
        {
            addSpecial();
            initialiseHand();
        }

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
        Debug.Log("Level: " + level);
        Debug.Log("Cards amount: " + cards.Count);

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
        if (specialInt == 5)
        {
            specialInt = 0;
        }

        for (var i = 0; i < specialInt; i++)
        {

            cards.Add(specialcards[i]);
        }

    }

}

