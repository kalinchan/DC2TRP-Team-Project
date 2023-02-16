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
        cardList.Add(new Card(0, "Submit Assignment", 1, "Attack", 4, 0, 2, "Attack: +2", false, Resources.Load<Sprite>("Sprites/Cards/Write")));
        cardList.Add(new Card(1, "Give Presentation", 2, "Attack", 4, 0, 3, "Attack: +3", false, Resources.Load<Sprite>("Sprites/Cards/Speak")));
        cardList.Add(new Card(2, "Sit Exam", 2, "Attack", 3, 0, 4, "Attack: +4", false, Resources.Load<Sprite>("Sprites/Cards/ExamQuiz")));
        cardList.Add(new Card(3, "Submit Report", 1, "Attack", 4, 0, 2, "Attack: +2", false, Resources.Load<Sprite>("Sprites/Cards/Write")));
        cardList.Add(new Card(4, "Mentor Students", 2, "Attack", 4, 0, 3, "Attack: +3", false, Resources.Load<Sprite>("Sprites/Cards/Speak")));
<<<<<<< Updated upstream
        cardList.Add(new Card(5, "Take Quiz", 2, "Attack", 2, 0, 3, "Attack: +3", false, Resources.Load<Sprite>("Sprites/Cards/ExamQuiz")));
=======
        cardList.Add(new Card(5, "Sit Quiz", 2, "Attack", 3, 0, 4, "Attack: +4", false, Resources.Load<Sprite>("Sprites/Cards/ExamQuiz")));
>>>>>>> Stashed changes

        // Defence Cards
        cardList.Add(new Card(6, "Read Books", 1, "Defence", 3, 4, 0, "Defence: +4", true, Resources.Load<Sprite>("Sprites/Cards/Books")));
        cardList.Add(new Card(7, "Read Jounral Articles", 2, "Defence", 2, 5, 0, "Defence: +5", true, Resources.Load<Sprite>("Sprites/Cards/Articles")));
        cardList.Add(new Card(8, "Make Connections", 3, "Defence", 2, 6, 0, "Defence: +6", true, Resources.Load<Sprite>("Sprites/Cards/Connections")));
<<<<<<< Updated upstream

        // Special Cards
=======
        // Special Cards

>>>>>>> Stashed changes
        cardList.Add(new Card(9, "Get Enough Sleep", 0, "Special", 1, 0, 0, "Health: +10", true, Resources.Load<Sprite>("Sprites/Cards/Alarm")));
        cardList.Add(new Card(10, "Drink Energy Drink", 0, "Special", 1, 0, 0, "Energy: +5", true, Resources.Load<Sprite>("Sprites/Cards/EnergyDrink")));
        cardList.Add(new Card(11, "Workout", 3, "Special", 1, 0, 0, "Each attack: +3", true, Resources.Load<Sprite>("Sprites/Cards/Workout")));
    }

}
