using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Version 1.0 / Date: 11/01/2023 / Author CH

// script for selecting card to play from hand - CH
public class SelectCard : MonoBehaviour
{
    public GameObject Card;
    private EntityStats currentEnemy;
    private ThisCard thisCard;
    private Hand playerHand;
    public void Start()
    {
        thisCard = GetComponent<ThisCard>();
        playerHand = GameObject.Find("Player").GetComponent<Hand>();
    }
    // when card is selected by player - CH
    // Sets card to active, the active status can then be used for targeting & energy checking - JD
    public void OnClick()
    {
        // TODO: Alter Stats (Health, Energy, Defence, Attack) - depending on card played
        //GetEnemy();
        //Card.SetActive(false); // remove card from hand
        playerHand.currentlySelectedCard = thisCard;
    }

    //The card knows what enemy it is looking at. - JD
    public void GetEnemy(EntityStats enemy)
    {
        currentEnemy = enemy;
    }

    //card tells the current enemy to take damage equal to its damage value - JD
    public void dealDamage()
    {
        currentEnemy.TakeDamage(thisCard.damage);
    }
}
