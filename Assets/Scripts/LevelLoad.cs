using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelLoad : MonoBehaviour
{
    public GameObject Card01, Card02, Card03, Card04, Card05, Card06, Card07, Card08, Card09, Card10, Card11, Card12;
    public GameObject PlayerArea;
    public Sprite Background01, Background02, Background03;
    public GameObject backgroundParent;
    
    List<Sprite> backgrounds = new List<Sprite>();
    List<GameObject> cards = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        // get all cards
        cards.AddRange(new List<GameObject>
            {
                Card01, Card02, Card03, Card04, Card05, Card06, Card07, Card08, Card09, Card10, Card11, Card12
        }
        );

        for (var i = 0; i < 5; i++)
        {
            GameObject playerCard = Instantiate(cards[Random.Range(0, cards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.transform.SetParent(PlayerArea.transform, false);
        }

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
}

