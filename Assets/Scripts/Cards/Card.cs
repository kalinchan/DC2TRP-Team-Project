using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
// Version 1.0 / Date: 09/01/2023 / Author CH

// Card class for card attribute initialisation - CH
public class Card
{
    public int id; //example: 0, 1, 2, 3... (9 cards total)
    public string cardName; // // different for player and enemy cards
    public int energyCost; // example: 3 - reduces energy per turn by 3
    public string group; // exmaple: Attack / Defence / Special
    public int rarity; // not used, other implementation added
    public int damage; // how much damage this card causes
    public int defence; // how much defence this card has
    public string cardDescription; // what does this card do?
    public bool usableWithoutTarget; // usable without dragging to enemy?
    public Sprite cardSprite; // card image sprite

    // set fields when declaring card - CH
    public Card(int Id, string CardName, int EnergyCost, string Group, int Rarity, int Defence, int Damage, string Description, bool UsableWithoutTarget, Sprite Sprite)
    {
        id = Id;
        cardName = CardName;
        energyCost = EnergyCost;
        group = Group;
        rarity = Rarity;
        defence = Defence;
        damage = Damage;
        cardDescription = Description;
        usableWithoutTarget = UsableWithoutTarget;
        cardSprite = Sprite;
    }

    // for enemy attack cards - appear on screen when enemy attacks - CH
    void setName(string name) 
    {
        cardName = name;
    }

    public string getName()
    {
        return cardName;
    }
}