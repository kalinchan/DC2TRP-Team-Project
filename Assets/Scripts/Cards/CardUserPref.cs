using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using TMPro;

//this is to store the deck and special cars in user preferecnces


public class CardUserPref : MonoBehaviour
{
    
    //public string name;
    //public int score;

    public GameObject Card01, Card02, Card03, Card04, Card05, Card06, Card07, Card08, Card09, Card10, Card11, Card12, Card13;

    public List<GameObject> cards = new List<GameObject>();
    public List<GameObject> specialcards = new List<GameObject>();
    public List<GameObject> userSpecialCards = new List<GameObject>();

    public GameObject SpecialCardArea;
    public int specialInt;
    

    // Convert a list of objects to a string separated by commas
    /*public string ConvertListToString(List<GameObject> objects)
    {
        string result = string.Empty;

        for (int i = 0; i < objects.Count; i++)
        {
            result += objects[i].name + ":" + objects[i].score;

            if (i < objects.Count - 1)
            {
                result += ",";
            }
        }

        return result;
    }

    // Convert a string to a list of objects using commas as separators
    public List<GameObject> ConvertStringToList(string data)
    {
        List<GameObject> objects = new List<MyGameObject>();

        string[] objectData = data.Split(',');

        foreach (string obj in objectData)
        {
            string[] properties = obj.Split(':');

            if (properties.Length == 2)
            {
                GameObject newObj = new MyGameObject();
                newObj.name = properties[0];
                newObj.score = int.Parse(properties[1]);

                objects.Add(newObj);
            }
        }

        return objects;
    }*/

    public void SelectRandomCard()
    {
        specialInt = Random.Range(0, specialcards.Count);

        // instantiate special card depending on which level is completed --
        GameObject specialCard = Instantiate(specialcards[specialInt], new Vector3(0, 0, 0), Quaternion.identity);
        specialCard.transform.SetParent(SpecialCardArea.transform, false);
        cards.Add(specialCard);
        specialcards.Remove(specialCard);

    }

    public void setCards()
    {
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
    }

    // Example usage
    void Start()
    {
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
    }
}
