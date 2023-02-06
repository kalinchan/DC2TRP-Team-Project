using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Version 1.0 / Date: 09/01/2023 / Author CH

// database for cards in deck - CH
public class CardDataBase : MonoBehaviour {
    public static List<Card> cardList = new List<Card>();

    void Awake()
    {
        // Attack Cards
        cardList.Add(new Card(0, "Submit Assignment", 1, "Attack", 4, 0, 2, "Attack: +2", false, Resources.Load<Sprite>("Sprites/Cards/Card1")));
        cardList.Add(new Card(1, "Give Presentation", 2, "Attack", 4, 0, 3, "Attack: +3", false, Resources.Load<Sprite>("Sprites/Cards/Card2")));
        cardList.Add(new Card(2, "Sit Exam", 2, "Attack", 3, 0, 4, "Attack: +4", false, Resources.Load<Sprite>("Sprites/Cards/Card3")));
        // Defence Cards
        cardList.Add(new Card(3, "Read Books", 1, "Defence", 3, 4, 0, "Defence: +4", true, Resources.Load<Sprite>("Sprites/Cards/Card4")));
        cardList.Add(new Card(4, "Read Jounral Articles", 2, "Defence", 2, 5, 0, "Defence: +5", true, Resources.Load<Sprite>("Sprites/Cards/Card5")));
        cardList.Add(new Card(5, "Make Connections", 3, "Defence", 2, 6, 0, "Defence: +6", true, Resources.Load<Sprite>("Sprites/Cards/Card6")));
        // Special Cards
        cardList.Add(new Card(6, "Get Enough Sleep", 0, "Special", 1, 0, 0, "Health: +10", true, Resources.Load<Sprite>("Sprites/Cards/Card7")));
        cardList.Add(new Card(7, "Drink Energy Drink", 0, "Special", 1, 0, 0, "Energy: +5", true, Resources.Load<Sprite>("Sprites/Cards/Card8")));
        cardList.Add(new Card(8, "Workout", 3, "Special", 1, 0, 0, "Each attack: +3", true, Resources.Load<Sprite>("Sprites/Cards/Card9")));
    }

}
