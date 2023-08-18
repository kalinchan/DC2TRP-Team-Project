using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Version 1.0 / Date: 09/01/2023 / Author CH

// database for cards in deck - CH
public class CardDataBase : MonoBehaviour {
    public static List<Card> cardList = new List<Card>();
    // Card(int Id, string CardName, int EnergyCost, string Group, int Rarity, int Defence, int Damage, string Description, bool UsableWithoutTarget, Sprite Sprite)
    void Awake()
    {
        // Attack Cards
        cardList.Add(new Card(0, "Submit Assignment", 2, "Attack", 0, 0, 3, " +3", false, Resources.Load<Sprite>("Sprites/Cards/Tick"))); // Attack +2
        cardList.Add(new Card(1, "Give Presentation", 3, "Attack", 0, 0, 4, " +4", false, Resources.Load<Sprite>("Sprites/Cards/Speak"))); // Attack +3
        cardList.Add(new Card(2, "Sit Exam", 4, "Attack", 0, 0, 5, " +5", false, Resources.Load<Sprite>("Sprites/Cards/ExamQuiz"))); // Attack +4
        cardList.Add(new Card(3, "Submit Report", 2, "Attack", 0, 0, 3, " +3", false, Resources.Load<Sprite>("Sprites/Cards/Write")));// Attack +2
        cardList.Add(new Card(4, "Mentor Students", 1, "Attack", 0, 0, 2, " +2", false, Resources.Load<Sprite>("Sprites/Cards/Mentor"))); // Attack +1
        cardList.Add(new Card(5, "Take Quiz", 3, "Attack", 0, 0, 4, " +4", false, Resources.Load<Sprite>("Sprites/Cards/ExamQuiz2"))); // Attack +3

        // Defence Cards
        cardList.Add(new Card(6, "Read Books", 2, "Defence", 0, 2, 0, " +2", true, Resources.Load<Sprite>("Sprites/Cards/Books"))); // Defence +2
        cardList.Add(new Card(7, "Read Jounral Articles", 3, "Defence", 0, 3, 0, " +3", true, Resources.Load<Sprite>("Sprites/Cards/Articles"))); // Defence +3
        cardList.Add(new Card(8, "Make Connections", 1, "Defence", 0, 1, 0, " +1", true, Resources.Load<Sprite>("Sprites/Cards/Connections"))); // Defence +1

        // Special Cards
        cardList.Add(new Card(9, "Get Enough Sleep", 0, "Special", 0, 0, 0, " +5", true, Resources.Load<Sprite>("Sprites/Cards/Alarm"))); // Health +5
        cardList.Add(new Card(10, "Drink Energy Drink", 0, "Special", 0, 0, 0, " +3", true, Resources.Load<Sprite>("Sprites/Cards/EnergyDrink"))); // Energy +3
        cardList.Add(new Card(11, "Workout", 4, "Special", 0, 0, 0, " x2", true, Resources.Load<Sprite>("Sprites/Cards/Workout"))); // Attack x2
        cardList.Add(new Card(12, "Extension", 3,"Special", 0, 5, 0, "+5", true, Resources.Load<Sprite>("Sprites/Cards/extend"))); // +5 defence
    }

}
