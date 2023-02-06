using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelLoad : MonoBehaviour
{
    public GameObject Card01, Card02, Card03, Card04, Card05, Card06, Card07, Card08, Card09;
    public GameObject PlayerArea;

    List<GameObject> cards = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        cards.AddRange(new List<GameObject>
            {
                Card01, Card02, Card03, Card04, Card05, Card06, Card07, Card08, Card09,
        }
        );

        for (var i = 0; i < 5; i++)
        {
            GameObject playerCard = Instantiate(cards[Random.Range(0, cards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.transform.SetParent(PlayerArea.transform, false);
        }

    }
}

